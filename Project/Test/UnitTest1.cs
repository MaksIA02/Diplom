using Project_BLL;
using DAL;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Project_DAL;
using System;

namespace TestLab3
{
    public class Tests
    {
        
        [Test]
        public void MockTestCard()
        {
            // arrange
            var mock = new Mock<IRepository<Guid, Card>>();
            mock.Setup(c => c.GetAll()).Returns(new List<Card>
            {
                new Card { Id = System.Guid.NewGuid(), Name ="test"}
            });


            // act
            var cards = mock.Object.GetAll();

            // assert            
            Assert.NotNull(cards);
            foreach (var c in cards)
            {
                if (c.Name == "test")
                {
                    Assert.AreEqual("test", c.Name);
                }
            }
        }
        [Test]
        public void MockTestArticle()
        {
            // arrange
            var mock = new Mock<IRepository<Guid, Article>>();
            mock.Setup(c => c.GetAll()).Returns(new List<Article>
            {
                new Article { Id = System.Guid.NewGuid(), Name ="test", Amount = 500}
            });


            // act
            var articles = mock.Object.GetAll();

            // assert            
            Assert.NotNull(articles);
            foreach (var a in articles)
            {
                if (a.Name == "test")
                {
                    Assert.AreEqual("test", a.Name);
                }
            }
        }
        [Test]
        public void MockTestTransfer()
        {
            // arrange
            var mock = new Mock<IRepository<Guid, Transfer>>();
            mock.Setup(c => c.GetAll()).Returns(new List<Transfer>
            {
                new Transfer { Id = System.Guid.NewGuid(), Name ="test", Amount = 500}
            });


            // act
            var transfers = mock.Object.GetAll();

            // assert            
            Assert.NotNull(transfers);
            foreach (var t in transfers)
            {
                if (t.Name == "test")
                {
                    Assert.AreEqual("test", t.Name);
                }
            }
        }




        // Інтеграційні
        readonly ILogic logic = new Project_BLL.BLL();

        [Test]
        public void CardCreateTest()
        {
            // arrange


            // act
            logic.AddCard("CreateTestCard");
            var cards = logic.ShowCard();
            // assert            
            foreach (Card c in cards)
            {
                if (c.Name == "CreateTestCard")
                {
                    Assert.AreEqual("CreateTestCard", c.Name);
                }
            }

        }

        [Test]
        public void ExchangeTest()
        {
            // arrange
            
            // act
            logic.AddCard("CreateTestCardExchange");
            logic.AddCard("testExchange");
            logic.MakeTransferFor2Crads("CreateTestCardExchange", "testExchange", 1000);
            var transfers = logic.ShowTransfer();
            // assert            
            foreach (Transfer t in transfers)
            {
                if (t.Amount == 1000)
                {
                    Assert.IsTrue(true);
                }
            }

        }

        [Test]
        public void CardDeleteTest()
        {
            // arrange
            
            // act
            logic.AddCard("DeleteCard");
            logic.AddCard("asdCard");
            logic.RemoveCard("DeleteCard");
            var cards = logic.ShowCard();
            // assert            
            foreach (Card c in cards)
            {
                if (c.Name == "DeleteCard")
                {
                    Assert.IsFalse(true);
                }
            }

        }
        [Test]
        public void ArticleCreateTest()
        {
            // arrange

            // act
            logic.AddArticle("TestArticle", 300);
            var articles = logic.ShowArticle();
            // assert            
            foreach (Article a in articles)
            {
                if (a.Name == "TestArticle")
                {
                    Assert.AreEqual("TestArticle", a.Name);
                }
            }

        }
        [Test]
        public void UseArticleCreateTest()
        {
            // arrange       

            // act
            logic.UseArticle("test", "TestArticle");
            var cards = logic.ShowCard();
            // assert            
            foreach (Card c in cards)
            {
                var newtransfer = c.Transfers;
                foreach (var t in newtransfer)
                {
                    if (t.Name == "TestArticle")
                    {
                        Assert.IsTrue(true);
                    }
                }
            }
        }
        [Test]
        public void ArticleDeleteTest()
        {
            // arrange
            var articles = logic.ShowArticle();

            // act
            logic.AddArticle("DeleteArticle", 500);
            logic.AddArticle("asddae", 500);
            logic.RemoveArticle("DeleteArticle");

            // assert            
            foreach (Article a in articles)
            {
                if (a.Name == "DeleteCard")
                {
                    Assert.IsFalse(true);
                }
            }

        }
        [Test]
        public void ShowIncomeTest()
        {
            // arrange


            // act
            logic.AddCard("TestCard2");
            logic.MakeTransfer("TestCard2", 5000);
            var cards = logic.ShowCard();
            decimal testvalue = logic.ShowCardIncome("TestCard2");
            decimal TrueValue = 0;
            // assert            
            foreach (Card c in cards)
            {
                if (c.Name == "TestCard2")
                {
                    var newincome = c.Incomes;
                    foreach (var i in newincome)
                    {
                        TrueValue += i.Amount;
                        if (testvalue == TrueValue)
                        {
                            Assert.IsTrue(true);
                        }
                    }
                }
            }

        }
        [Test]
        public void ShowExpenseTest()
        {
            // arrange
            decimal TrueValue = 0;

            // act
            logic.AddCard("TestCard3");
            logic.MakeTransfer("TestCard3", -5000);
            var cards = logic.ShowCard();
            decimal testvalue = logic.ShowCardExpense("TestCard3");

            // assert            
            foreach (Card c in cards)
            {
                if (c.Name == "TestCard3")
                {
                    var newexpense = c.Expenses;
                    foreach (var e in newexpense)
                    {
                        TrueValue += e.Amount;
                        if (testvalue == TrueValue)
                        {
                            Assert.IsTrue(true);
                        }
                    }
                }
            }

        }
        [Test]
        public void ShowArtStatTest()
        {
            // arrange
            decimal TrueValue = 0;

            // act
            logic.AddCard("TestCard4");
            logic.AddArticle("TestArticle2", 500);
            logic.UseArticle("TestCard4", "TestArticle2");
            var cards = logic.ShowCard();
            var articles = logic.ShowArticle();
            decimal testvalue = logic.ShowCardAndArticleStat("TestCard4", "TestArticle2");

            // assert            
            foreach (Card c in cards)
            {
                if (c.Name == "TestCard4")
                {
                    foreach (Article a in articles)
                    {
                        var newtransfers = c.Transfers;
                        foreach (var t in newtransfers)
                        {
                            if (t.Name == a.Name)
                            {
                                TrueValue += t.Amount;
                                if (testvalue == TrueValue)
                                {
                                    Assert.IsTrue(true);
                                }
                            }
                            
                        }
                    }
                }
            }

        }

        
    }
}