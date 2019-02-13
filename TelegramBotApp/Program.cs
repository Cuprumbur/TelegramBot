using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBotApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var states = new List<State>();
            //bot = new Bot

            //update +=
            // {
            //    var fsm = new Fsm()
            // }
        }
    }

    public class Fsm
    {
        public Fsm(int chatId, List<State> states, object msg, object bot)
        {
            var state = states.FirstOrDefault(x => x.ChatId == chatId);
            if (state == null)
            {
                state = new State() { ChatId = chatId, StateChat = StateChat.Main };
                states.Add(state);
            }

            IUpdateState updateState;
            switch (state.StateChat)
            {
                case StateChat.Main:
                    updateState = new MainState();
                    break;

                case StateChat.Dialog:
                    updateState = new Dialog();
                    break;

                default:
                    throw new AggregateException();
            }
            updateState.Update(msg, bot, chatId, state);
        }
    }

    public class Dialog : IUpdateState
    {
        public void Update(object msg, object bot, int chatId, State state)
        {
            if (msg == "У меня все хорошо!!!")
            {
                //boi.Send(chatId, "Это здорово, рад за тебя!!!")
            }
            else if (msg == "Домой")
            {
                state.StateChat = StateChat.Main;
            }
            else
            {
                //boi.Send(chatId, "Не понимсаю тебя, если хочешь на главную прсото скажи "Домой"")
            }
        }
    }

    public class MainState : IUpdateState
    {
        public void Update(object msg, object bot, int chatId, State state)
        {
            if (msg == "Привет")
            {
                //boi.Send(chatId, "Hi ,men, Как дела?")
                state.StateChat = StateChat.Dialog;
            }
            else
            {
                //boi.Send(chatId, "Ввод неверной информации, повторитие")
            }
        }
    }

    internal interface IUpdateState
    {
        void Update(object msg, object bot, int chatId, State state);
    }

    public class State
    {
        public int ChatId { get; set; }
        public StateChat StateChat { get; set; }
    }

    public enum StateChat
    {
        Main,
        Dialog
    }
}