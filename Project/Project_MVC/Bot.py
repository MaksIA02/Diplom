import sqlite3
import nest_asyncio
from telegram import Update
from telegram.ext import Application, CommandHandler, CallbackContext, MessageHandler, filters

# Введіть токен вашого бота
TOKEN = '7169949826:AAE5z7uoFM7NH8LMquBe2QcD67iiXfaL56U'

# Глобальна змінна для зберігання логіну користувача
user_login = None

async def start(update: Update, context: CallbackContext) -> None:
    await update.message.reply_text('Привіт! Використовуйте команду /setid <Ваш ID> для введення вашого ID.')

def get_login_from_db(user_id: str) -> str:
    # Підключення до бази даних SQLite
    conn = sqlite3.connect('Cards.db')
    cursor = conn.cursor()
    
    # Виконання запиту для отримання логіну
    cursor.execute('SELECT UserName FROM AspNetUsers WHERE Id = ?', (user_id,))
    row = cursor.fetchone()
    
    # Закриття з'єднання
    conn.close()
    
    return row[0] if row else None

def get_data_from_db(user_login: str) -> str:
    # Підключення до бази даних SQLite
    conn = sqlite3.connect('Cards.db')
    cursor = conn.cursor()
    
    # Виконання запиту для отримання даних
    cursor.execute('SELECT Name, Amount, Currency, Goal, GoalDate FROM Cards WHERE IdentityUser = ?', (user_login,))
    rows = cursor.fetchall()
    
    # Форматування результатів
    result = ''
    for row in rows:
        result += f"Назва: {row[0]}, Сума: {row[1]}, Валюта: {row[2]}, Мета: {row[3]}, Дата мети: {row[4]}\n"
    
    # Закриття з'єднання
    conn.close()
    
    return result if result else 'Немає даних для цього логіну користувача.'

async def setid(update: Update, context: CallbackContext) -> None:
    global user_login
    if len(context.args) != 1:
        await update.message.reply_text('Будь ласка, введіть свій ID у форматі: /setid <Ваш ID>')
        return
    
    user_id = context.args[0]
    user_login = get_login_from_db(user_id)
    
    if user_login:
        await update.message.reply_text(f'Ваш логін встановлено: {user_login} переглянте інформацію за допомогою команди /getdata')
    else:
        await update.message.reply_text('Невірний ID. Будь ласка, спробуйте ще раз.')

async def getdata(update: Update, context: CallbackContext) -> None:
    global user_login
    if not user_login:
        await update.message.reply_text('Будь ласка, спочатку встановіть свій ID за допомогою команди /setid <Ваш ID>.')
        return
    
    # Отримання даних з бази даних
    data = get_data_from_db(user_login)
    
    # Відправка даних у чат
    await update.message.reply_text(f'Дані про рахунки:\n{data}')

async def main() -> None:
    # Створення додатку та передача токену бота
    application = Application.builder().token(TOKEN).build()
    
    # Реєстрація команд
    application.add_handler(CommandHandler("start", start))
    application.add_handler(CommandHandler("setid", setid))
    application.add_handler(CommandHandler("getdata", getdata))
    
    # Запуск бота
    print("Bot is running...")
    await application.run_polling()

if __name__ == '__main__':
    import asyncio

    # Використання nest_asyncio для вирішення проблеми з вже запущеним циклом подій
    nest_asyncio.apply()

    # Перевірка, чи цикл подій вже запущений
    try:
        loop = asyncio.get_running_loop()
    except RuntimeError:
        loop = None

    if loop and loop.is_running():
        print('Running in an existing event loop')
        # Запуск асинхронної головної функції в наявному циклі подій
        asyncio.ensure_future(main())
    else:
        # Запуск асинхронної головної функції в новому циклі подій
        asyncio.run(main())
