namespace ex1;

public class Program
    {
        static void Main(string[] args)
        {
            List<City> cities = new List<City>
            {
                new City("Город А", 0),
                new City("Город Б", 0),
                new City("Город В", 0),
                new City("Город Г", 0),
                new City("Город Д", 0),
                new City("Город Е", 0),
                new City("Город Ж", 0)
            };
            Trader trader = new Trader(name: "Иван", money: new Random().Next(100, 201), capacity: 50);
            
            List<Goods> availableGoods = new List<Goods>();
            Random rnd = new Random();
            
            for (int i = 0; i < 10; i++)
            {
                GoodsType randomType = (GoodsType)rnd.Next(Enum.GetNames(typeof(GoodsType)).Length);
                double baseCost = rnd.Next(5, 20);
                double weight = rnd.Next(1, 10);
                IQualityState initialState = new NormalState();

                Goods newGoods = new Goods(randomType, baseCost, weight, initialState);
                availableGoods.Add(newGoods);
            }

            Console.WriteLine($"У торговца начальные деньги: {trader.Money:F2}, " +
                              $"грузоподъёмность: {trader.Capacity}, скорость: {trader.Speed}");
            
            foreach (var g in availableGoods)
            {
                try
                {
                    trader.BuyGoods(g);
                    Console.WriteLine($"Куплен товар: {g.Type}, цена: {g.BaseCost}, вес: {g.Weight}, качество: {g.QualityState.Quality}");
                }
                catch (NotEnoughMoneyException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (OverCapacityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            
            City destination = cities[rnd.Next(cities.Count)];
            destination.Distance = rnd.Next(50, 101);

            Console.WriteLine($"\nТорговец отправляется в {destination.Name}, " +
                              $"расстояние до города: {destination.Distance} лиг.");

            double distanceCovered = 0;
            
            List<EventType> possibleEvents = new List<EventType>
            {
                EventType.Обычный_день,
                EventType.Дождь,
                EventType.Ровная_дорога,
                EventType.Сломалось_колесо,
                EventType.Река,
                EventType.Встретил_местного,
                EventType.Разбойники,
                EventType.Придорожный_трактир,
                EventType.Товар_испортился
            };
            
            while (true)
            {
                if (distanceCovered >= destination.Distance)
                {
                    Console.WriteLine($"\nТорговец добрался до города {destination.Name}!");
                    break;
                }

                Console.WriteLine($"\nТекущий прогресс: {distanceCovered}/{destination.Distance}, скорость: {trader.Speed}");
                
                EventType currentEvent = possibleEvents[rnd.Next(possibleEvents.Count)];
                Console.WriteLine($"Событие: {currentEvent}.");
                
                double dailySpeed = trader.Speed;
                switch (currentEvent)
                {
                    case EventType.Обычный_день:
                        break;
                    case EventType.Дождь:
                        trader.Speed = Math.Max(trader.Speed - 2, 1);
                        if (rnd.NextDouble() < 0.3 && trader.GoodsList.Count > 0)
                        {
                            int index = rnd.Next(trader.GoodsList.Count);
                            trader.GoodsList[index].DegradeQuality();
                            Console.WriteLine($"Во время дождя товар {trader.GoodsList[index].Type} " +
                                              $"испортился. Новое качество: {trader.GoodsList[index].QualityState.Quality}");
                        }
                        dailySpeed = trader.Speed;
                        break;
                    case EventType.Ровная_дорога:
                        trader.Speed = Math.Min(trader.Speed + 2, 5);
                        dailySpeed = trader.Speed;
                        break;
                    case EventType.Сломалось_колесо:
                        dailySpeed = 0;
                        Console.WriteLine("Целый день потрачен на починку колеса. Скорость = 0.");
                        break;
                    case EventType.Река:
                        dailySpeed = 0;
                        Console.WriteLine("День ушёл на поиски брода. Скорость = 0.");
                        break;
                    case EventType.Встретил_местного:
                        int bonus = rnd.Next(3, 7);
                        dailySpeed = trader.Speed + bonus;
                        Console.WriteLine($"Встретили местного, который подсказал короткий путь, " +
                                          $"дополнительно проехали: {bonus} лиг.");
                        break;
                    case EventType.Разбойники:
                        if (trader.Money > 20)
                        {
                            trader.Money -= 20;
                            Console.WriteLine("Откупились разбойникам за 20 монет.");
                        }
                        else
                        {
                            if (trader.GoodsList.Count > 0)
                            {
                                Goods bestGoods = trader.GoodsList[0];
                                double bestValue = bestGoods.GetFinalCost();
                                foreach (var g in trader.GoodsList)
                                {
                                    double val = g.GetFinalCost();
                                    if (val > bestValue)
                                    {
                                        bestValue = val;
                                        bestGoods = g;
                                    }
                                }
                                trader.GoodsList.Remove(bestGoods);
                                Console.WriteLine($"Разбойники забрали товар: {bestGoods.Type}");
                            }
                            else
                            {
                                Console.WriteLine("Нечего забирать, разбойники ушли ни с чем...");
                            }
                        }
                        break;
                    case EventType.Придорожный_трактир:
                        Console.WriteLine("Остановка в придорожном трактире.");
                        double oldMoney = trader.Money;
                        trader.SellAllGoods(1.1); 
                        Console.WriteLine($"Товар продан за повышенную цену. Деньги были: {oldMoney}, стали: {trader.Money}");
                        break;
                    case EventType.Товар_испортился:
                        if (trader.GoodsList.Count > 0)
                        {
                            int index = rnd.Next(trader.GoodsList.Count);
                            trader.GoodsList[index].DegradeQuality();
                            Console.WriteLine($"Товар {trader.GoodsList[index].Type} испортился. " +
                                              $"Новое качество: {trader.GoodsList[index].QualityState.Quality}");
                        }
                        break;
                }
                distanceCovered += dailySpeed;
            }
            
            double moneyBeforeSell = trader.Money;
            trader.SellAllGoods(1.0);
            double moneyAfterSell = trader.Money;

            Console.WriteLine($"Перед продажей в {destination.Name} у торговца было: {moneyBeforeSell:F2} монет.");
            Console.WriteLine($"После продажи стало: {moneyAfterSell:F2} монет.");
            if (moneyAfterSell > moneyBeforeSell)
            {
                Console.WriteLine("Торговец в прибыли!");
            }
            else if (moneyAfterSell < moneyBeforeSell)
            {
                Console.WriteLine("Торговец в убытке...");
            }
            else
            {
                Console.WriteLine("Торговец вышел в ноль.");
            }

            Console.WriteLine("\nСимуляция завершена. Нажмите любую клавишу для выхода.");
            Console.ReadKey();
        }
    }