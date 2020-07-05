using System;

namespace ChainOfResponsibilityPattern
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(object request);
    }
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            this._nextHandler = handler;

            // Возврат обработчика отсюда позволит связать обработчики простым
            // способом, вот так:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Handle(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Handle(request);
            }
            else
            {
                return null;
            }
        }
    }

    public class AllUserHandler : AbstractHandler
    {
        public AllUserHandler() : base()
        {
        }

        public override object Handle(object request)
        {
            if ((request as string) == "all")
            {
                Console.WriteLine("Сообщение отправлено для всех пользователей");
                return base.Handle(request);
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
    public class AdminHandler : AbstractHandler
    {
        public AdminHandler() : base()
        {
        }

        public override object Handle(object request)
        {
            if ((request as string) == "admin")
            {
                Console.WriteLine("Сообщение отправлено для администраторов");
                return base.Handle(request);
            }
            else if ((request as string) == "all")
            {
                Console.WriteLine("(Администратор) Сообщение отправлено для всех");
                return base.Handle(request);
            }
            else 
            {
                return base.Handle(request);
            }
        }
    }

    public class Chat
    {
        public void StartChat(IHandler handler)
        {
            string allMsg = "all";
            string adminMsg = "admin";

            handler.Handle(adminMsg);
            handler.Handle(allMsg);

            Console.Read();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IHandler allHandler = new AllUserHandler();
            IHandler adminHandler = new AdminHandler();

            allHandler.SetNext(adminHandler);

            Chat chat = new Chat();
            chat.StartChat(allHandler);
        }
    }
}
