using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KimBab
{
    public class MainFunction
    {
        static void Main()
        {
            int totalTimer = 0; // 게임 속도
            int customerTimer = 0; // 손님 증가 타이머
            int custormerSetTimer = 0; // 손님 배치 타이머
            int cookingTimer = 0; // 요리 타이머
            int orderTimer = 0; // 주문 받는 타이머
            int eatingTimer = 0; // 식사 시간 타이머
            int cleaningTimer = 0; // 청소 시간 타이머
            int dayTimer = 0; // 날짜 타이머
            int currentTimer; // 현재 시간

            int windowWidth = 100;
            int windowHeight = 40;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);
            // ===================================== 게임 기본 입력 3종 받기
            GameManager.Instance.InitGame(); // 입력
            GameManager.Instance.InitTableSize(); // 테이블 크기 할당
            Console.Clear();
            // ===================================== 직원 이름 초기화
            GameManager.Instance.GetOwner.InitMyName("사장");
            GameManager.Instance.GetChef.InitMyName("주방");
            GameManager.Instance.GetKimbabChef.InitMyName("김밥");
            GameManager.Instance.GetServe.InitMyName("서빙");
            // ===================================== 정보 표시
            Console.CursorVisible = false;
            currentTimer = Environment.TickCount & Int32.MaxValue;
            dayTimer = currentTimer;

            while(true)
            {
                currentTimer = Environment.TickCount & Int32.MaxValue;
                if(currentTimer - totalTimer > 1000) // 게임 전체 속도 : 가장 짧은 초기화 시간을 기준으로
                {
                    totalTimer = currentTimer;
                    Console.Clear();
                    GameManager.Instance.ViewInformation(); // 가게 정보 표시
                    GameManager.Instance.ViewWorker(); // 직원 정보 표시
                    GameManager.Instance.ViewTable(); // 테이블 정보 표시
                    GameManager.Instance.ViewWaiting(); // 대기 손님 표시
                }
                if(currentTimer - customerTimer > 2000) // 2초마다 손님 증가
                {
                    customerTimer = currentTimer;
                    GameManager.Instance.MakeCustomer();
                }
                if(currentTimer - custormerSetTimer > 4000) // 4초마다 배치
                {
                    custormerSetTimer = currentTimer;
                    GameManager.Instance.SetCustomer();
                }
                if(currentTimer - orderTimer > 5000) // 5초마다 주문
                {
                    orderTimer = currentTimer;
                    GameManager.Instance.GetOrder();
                }
                if(currentTimer - cookingTimer > 1000) // 1초마다 요리 시간 체크
                {
                    cookingTimer = currentTimer;
                    GameManager.Instance.MakeDish();
                    GameManager.Instance.MakeKimbab();
                }
                if(currentTimer - eatingTimer > 1000) // 1초마다 식사 종료 체크
                {
                    eatingTimer = currentTimer;
                    GameManager.Instance.FinishEating();
                }
                if(currentTimer - cleaningTimer > 1000) // 1초마다 청소체크
                {
                    cleaningTimer = currentTimer;
                    GameManager.Instance.Cleaning();
                    GameManager.Instance.FinishCleaning();
                }
                if(currentTimer - dayTimer > 60000) // 1분 마다
                {
                    dayTimer = currentTimer;
                    GameManager.Instance.PlusDay(); // 날짜 증가
                }
            }
        }
    }
}
