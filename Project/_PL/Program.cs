using Project_BLL;
using Ninject;

Console.WriteLine("Щоб додати рахунок напишiть: add");
Console.WriteLine("Щоб переглянути рахунки напишiть: show");
Console.WriteLine("Щоб видалити рахунок напишiть: del");
Console.WriteLine("Щоб змiнити кiлькiсть грошей напишiть: maketransfer");
Console.WriteLine("Щоб перевести грошi на iнший рахунок напишiть: exchange");
Console.WriteLine("Щоб додати статтю напишiть: addart");
Console.WriteLine("Щоб додати/витратити кошти за статею напишiть: useart");
Console.WriteLine("Щоб переглянути статтi напишiть: showart");
Console.WriteLine("Щоб видалити статтю напишiть: delart");
Console.WriteLine("Щоб переглянути прибутки рахунку напишiть: showincome");
Console.WriteLine("Щоб переглянути прибутки рахунку по статтi напишiть: showartstat");
Console.WriteLine("Щоб переглянути витрати рахунку напишiть: showexp");
Console.WriteLine("Для виходу напишiть: q");

IKernel ninjectKernel = new StandardKernel(new IoC_BLL());
var logic = ninjectKernel.Get<ILogic>();

while (true)
{
    switch (Console.ReadLine())
    {
        case ("show"):
            Console.WriteLine("Список рахункiв: ");
            var cards = logic.ShowCard();
            foreach (var c in cards)
            {
                Console.WriteLine($"{c.Name} - {c.Amount}");
            }

            break;

        case ("q"):

            Environment.Exit(0);
            break;

        case ("add"):
            Console.WriteLine("Введiть назву рахунку");
            try
            {
                logic.AddCard(Console.ReadLine());
                Console.WriteLine("Список рахункiв: ");
                var cards2 = logic.ShowCard();
                foreach (var c in cards2)
                {
                    Console.WriteLine($"{c.Name} - {c.Amount}");
                }
            }
            catch
            {
                Console.WriteLine("Сталася помилка");
                break;
            }         
            break;

        case ("del"):

            Console.WriteLine("Введiть назву рахунку");
            logic.RemoveCard(Console.ReadLine());
            break;

        case ("maketransfer"):

            Console.WriteLine("Введiть назву рахунку та суму");
            try
            {
                logic.MakeTransfer(Console.ReadLine(), Convert.ToDecimal(Console.ReadLine()));
            }
            catch
            {
                Console.WriteLine("Сталася помилка");
                break;
            }
            Console.WriteLine("Переказ успiшний");
            break;

        case ("exchange"):

            Console.WriteLine("Введiть назву рахунку з якого хочете перевести а потiм рахунок на який хочете перевести та суму");
            try
            {
                logic.MakeTransferFor2Crads(Console.ReadLine(), Console.ReadLine(), Convert.ToDecimal(Console.ReadLine()));
            }
            catch
            {
                Console.WriteLine("Сталася помилка");
                break;
            }
            break;

        case ("addart"):

            Console.WriteLine("Введiть назву статтi та цiну");
            try
            {
                logic.AddArticle(Console.ReadLine(), Convert.ToDecimal(Console.ReadLine()));
            }
            catch 
            {
                Console.WriteLine("Сталася помилка");
                break;
            }
            Console.WriteLine("Створено статтю");
            break;

        case ("useart"):

            Console.WriteLine("Введiть назву картки та статтi");
            logic.UseArticle(Console.ReadLine(), Console.ReadLine());
            break;

        case ("showart"):

            Console.WriteLine("Наявнi статтi та суми");
            var articles = logic.ShowArticle();
            foreach (var a in articles)
            {
                Console.WriteLine($"{a.Name} - {a.Amount}");
            }
            break;

        case ("delart"):

            Console.WriteLine("Введiть назву статтi");
            logic.RemoveArticle(Console.ReadLine());
            break;

        case ("showincome"):

            Console.WriteLine("Введiть назву рахунку");
            Console.WriteLine(logic.ShowCardIncome(Console.ReadLine()) + " Загальний прибуток по цьому рахунку");
            break;

        case ("showartstat"):

            Console.WriteLine("Введiть назву рахунку та статтi");
            Console.WriteLine(logic.ShowCardAndArticleStat(Console.ReadLine(), Console.ReadLine()) + " Загальний прибуток/видатки по цьому рахунку та цiй статтi");
            break;

        case ("showexp"):

            Console.WriteLine("Введiть назву рахунку");
            Console.WriteLine(logic.ShowCardExpense(Console.ReadLine()) + " Загальнi видатки по цьому рахунку");
            break;
    }
}
