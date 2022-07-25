using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class GameManager : Singleton<GameManager>
    {
        private int Day; // 총 시뮬레이션 날짜
        private int CurrentDay = 1; // 현재 날짜
        private int TotalTableCount; // 총 테이블 수
        private int TotalWaitingCount; // 대기 손님 수 최대
        private int WaitingCount = 0; // 현재 대기 손님
        private int CurrentCustomer = 0; // 현재 손님 최대 수
        private int TodayRevenue = 0; // 오늘 수익
        private int TotalRevenue = 0; // 총 수익
        private int TodayVisitor = 0; // 오늘 방문
        private int TotalVisitor = 0; // 총 방문

        private int kimbabTimer = 0; // 요리타이머
        private int chefTimer = 0;

        // 돈계산
        private int man;
        private int thousand;
        private int left, left2;

        private Random rand = new Random();

        private Owner owner = new Owner(); // 주인
        public Owner GetOwner { get { return owner; } }
        private KimbabChef kimbabChef = new KimbabChef(); // 김밥 전문가
        public KimbabChef GetKimbabChef { get { return kimbabChef; } }
        private Serving serve = new Serving(); // 서빙
        public Serving GetServe { get { return serve; } }
        private Chef chef = new Chef(); // 요리 전문가
        public Chef GetChef { get { return chef; } }

        private Table[] tables; // 테이블
        private Menu menu = new Menu(); // 메늏
        public Menu GetMenu { get { return menu; } }

        public void InitGame() // 게임 기본 값 입력 받기
        {
            // ======================================= 날짜 수 입력
            while(true)
            {
                Console.Write("가게를 운영할 총 날짜 수를 입력하세요 : ");
                var day = int.Parse(Console.ReadLine());
                if (day > 0 && day < 100)
                {
                    Day = day;
                    break;
                }
                else Error(0);
            }
            // ======================================= 테이블 수 입력
            while(true)
            {
                Console.Write("가게에 배치할 총 테이블 수를 입력하세요 : ");
                var table = int.Parse(Console.ReadLine());
                if (table > 0 && table < 100)
                {
                    TotalTableCount = table;
                    break;
                }
                else Error(1);
            }
            // ======================================= 손님 수 입력
            while(true)
            {
                Console.Write("대기 가능한 최대 손님 수를 입력하세요 : ");
                var customer = int.Parse(Console.ReadLine());
                if (customer > 0 && customer < 100)
                {
                    TotalWaitingCount = customer;
                    break;
                }
                else Error(2);
            }
        }
   
        public void Error(int errorNum) // 입력 에러 처리
        {
            switch(errorNum)
            {
                case 0: // 날짜 에러
                    {
                        Console.WriteLine("날짜 ERROR : 0보다 크고 100보다 작은 수를 입력하세요.");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }
                case 1: // 테이블 수 에러
                    {
                        Console.WriteLine("테이블 ERROR : 0보다 크고 100보다 작은 수를 입력하세요.");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }
                case 2: // 대기 손님 수 에러
                    {
                        Console.WriteLine("손님 수 ERROR : 0보다 크고 100보다 작은 수를 입력하세요.");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                    }
            }
        }
        public void ViewInformation() // 가게 정보 표시
        {
            Console.Write("날짜 : " + CurrentDay + "일차  " + "매출(오늘 / 누적) : " + MoneyTranslate(TodayRevenue) + " / " + MoneyTranslate(TotalRevenue) + "  ");
            Console.WriteLine("방문객(오늘 / 누적) : " + TodayVisitor + " / " + TotalVisitor);
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine();
        }
        public void ViewWorker() // 직원 정보 표시
        {
            Console.WriteLine(owner.Name + " : " + owner.GetMyState());
            Console.WriteLine(chef.Name + " : " + chef.GetMyState());
            Console.WriteLine(kimbabChef.Name + " : " + kimbabChef.GetMyState());
            Console.WriteLine(serve.Name + " : " + serve.GetMyState());
            Console.WriteLine();
        }
        public void ViewTable() // 테이블 정보 표시
        {
            for(int i = 0; i < TotalTableCount; i++)
            {
                Console.WriteLine(tables[i].tableNumber + " : " + tables[i].state);
            }
        }
        public void ViewWaiting() // 대기 손님 표시
        {
            Console.WriteLine(".");
            Console.WriteLine(".");
            Console.WriteLine(".");
            Console.WriteLine("대기 중인 손님 수 : " + WaitingCount);
        }
        private string MoneyTranslate(int money) // 돈 단위 표시
        {
            if (money >= 0 && money < 1000)
            {
                return money.ToString() + "원";
            }
            else if (money >= 1000 && money < 10000)
            {
                thousand = money / 1000;
                left = money - (thousand * 1000);
                if (left == 0) return thousand + "천원";
                else return thousand.ToString() + "천" + left.ToString() + "원";
            }
            else if (money >= 10000 && money < 100000)
            {
                man = money / 10000;
                left = money - man * 10000;
                thousand = left / 1000;
                left2 = left - thousand * 1000;
                if (left == 0) return man.ToString() + "만원";
                else if (thousand == 0) return man.ToString() + "만" + left2.ToString() + "원";
                else if (left2 == 0) return man.ToString() + "만" + thousand.ToString() + "천원";
                else return man.ToString() + "만" + thousand.ToString() + "천" + left2.ToString() + "원";
            }
            else return money.ToString();
        }
        public void InitTableSize() // 테이블 크기 초기화
        {
            tables = new Table[TotalTableCount];
            for(int i = 0; i < TotalTableCount; i++)
            {
                tables[i] = new Table();
                tables[i].tableNumber = i + 1 + "번 테이블";
            }
        }
        public void MakeCustomer() // 일정 시간 손님 수 증가 함수
        {
            if(WaitingCount < TotalWaitingCount)
            {
                WaitingCount++; // 손님 증가
            }
        }
        public void SetCustomer() // 손님 주문 대기 상태로 배치
        {
            if(WaitingCount > 0)
            {
                for(int i = 0; i < TotalTableCount; i++)
                {
                    if (tables[i].isUsing == false)
                    {
                        CurrentCustomer++; // 현재 손님 수 증가
                        WaitingCount--; // 대기 손님 수 감소
                        tables[i].SwitchTableState(Table.TableState.waitOrder, "손님" + (TodayVisitor + 1).ToString(), 0); // 빈 테이블에 배치
                        TodayVisitor++;
                        TotalVisitor++;
                        break;
                    }
                }
            }
        }
        public void GetOrder() // 서빙 주문 받기
        {
            if(CurrentCustomer > 0)
            {
                for(int i = 0; i < TotalTableCount; i++)
                {
                    if (tables[i].now == Table.TableState.waitOrder) // 주문 기다리는 중이면
                    {
                        serve.SetMyFocus(tables[i].tableNumber); // 테이블 지정
                        serve.DoMyWork(Serving.ServeState.ordering); // 서빙해라
                        tables[i].SwitchTableState(Table.TableState.waitDish, tables[i].person, rand.Next((int)Menu.Food.normal, (int)Menu.Food.kimchi + 1)); // 메뉴받아
                        break;
                    }
                    else
                    {
                        serve.DoMyWork(Serving.ServeState.none);
                    }
                }
            }
        }
        public void MakeDish() // 요리하기
        {
            if(CurrentCustomer > 0)
            {
                for(int i = 0; i < TotalTableCount; i++)
                {
                    if (tables[i].now == Table.TableState.waitDish) // 요리 대기중이라면
                    {
                        if ((int)tables[i].nowMenu > 3) // 주방 메뉴라면
                        {
                            chef.DoMyWork(tables[i].nowMenu); // 주방 일해라
                            if(chefTimer < chef.Timer) // 현재 요리 시간보다 작다면
                            {
                                chefTimer++; // 1초마다 호출
                            }
                            else // 요리가 다되면
                            {
                                chefTimer = 0;
                                chef.DoMyWork(Menu.Food.none); // 요리가 나간것으로 변경
                                tables[i].SwitchTableState(Table.TableState.eating, tables[i].person, (int)tables[i].nowMenu);
                            }
                            break;
                        }
                    }
                    else
                    {
                        chef.DoMyWork(Menu.Food.none);
                    }
                }
            }
        }
        public void MakeKimbab() // 김밥싸기
        {
            if (CurrentCustomer > 0)
            {
                for (int i = 0; i < TotalTableCount; i++)
                {
                    if (tables[i].now == Table.TableState.waitDish)
                    {
                        if ((int)tables[i].nowMenu > 0 && (int)tables[i].nowMenu < 5) //  김밥이면
                        {
                            kimbabChef.DoMyWork(tables[i].nowMenu); // 김밥 말아라
                            if(kimbabTimer < kimbabChef.Timer)
                            {
                                kimbabTimer++;
                            }
                            else // 다 했으면
                            {
                                kimbabTimer = 0;
                                kimbabChef.DoMyWork(Menu.Food.none);
                                tables[i].SwitchTableState(Table.TableState.eating, tables[i].person, (int)tables[i].nowMenu); // 메뉴 나가라
                            }
                            break;
                        }
                    }
                    else
                    {
                        kimbabChef.DoMyWork(Menu.Food.none);
                    }
                }
            }
        }
        public void FinishEating() // 먹는거 끝내기 (1초마다 호출)
        {
            if(CurrentCustomer > 0)
            {
                for(int i = 0; i < TotalTableCount; i++)
                {
                    if (tables[i].now == Table.TableState.eating) // 먹는 중이라면
                    {
                        if (tables[i].timer < 5) // 다 안먹었다면
                        {
                            tables[i].timer++; // 1초 증가
                        }
                        else // 다 먹었다면
                        {
                            TodayRevenue += tables[i].price;
                            TotalRevenue += tables[i].price;
                            tables[i].SwitchTableState(Table.TableState.needClean, "none", 0); // 청소 필요로 변경
                            tables[i].timer = 0;
                        }
                    }
                }
            }
        }
        public void Cleaning() // 청소해라
        {
            for (int i = 0; i < TotalTableCount; i++)
            {
                if (tables[i].now == Table.TableState.needClean && owner.isWorking == false) // 청소가 필요하면 && 사장 놀고있으면
                {
                    owner.isWorking = true;
                    owner.SetMyFocus(tables[i].tableNumber);
                    owner.DoMyWork(Owner.OwnerState.cleaning); // 청소해라
                    tables[i].SwitchTableState(Table.TableState.cleaning, owner.Name, 0); // 청소 중
                    break;
                }
            }
        }
        public void FinishCleaning()
        {
            for (int i = 0; i < TotalTableCount; i++)
            {
                if (tables[i].now == Table.TableState.cleaning) // 청소 중 이라면
                {
                    if (owner.timer < 3)
                    {
                        owner.timer++;
                    }
                    else
                    {
                        tables[i].SwitchTableState(Table.TableState.empty, "none", 0); // 청소끝
                        owner.DoMyWork(Owner.OwnerState.none);
                        owner.timer = 0;
                        owner.isWorking = false;
                    }
                    break;
                }
            }
        }
        public void PlusDay() // 날짜 세기
        {
            if(CurrentDay <= Day) // 총 시뮬레이션 날짜까지
            {
                CurrentDay++; // 더하기
                TodayRevenue = 0;
                TodayVisitor = 0;
            }
        }
    }

}
