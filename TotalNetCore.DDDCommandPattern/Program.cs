using Autofac;
using System;
using System.Threading.Tasks;

namespace TotalNetCore.DDDCommandPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    //Marker interface
    public interface ICommand
    {

    }

    //命令的处理
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }

    //命令的发出
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }

    /// <summary>
    /// 命令分发的本质是把Command交给某个CommandHandler
    /// </summary>
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command), "Command can not be null");
            }

            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }

    public class CreateUser : ICommand
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Culture { get; set; }
    }

    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        public Task HandleAsync(CreateUser command)
        {
            //await _userService.RegisterAsync(command.UserId, command.Email, command.Password, command.Nickname, command.Culture);
            return Task.CompletedTask;
        }
    }

}
