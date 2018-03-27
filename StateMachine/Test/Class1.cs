using System.Collections.Generic;

namespace Game.Shared.Utility.StateMachine.Test
{
    //Our class for receiving messages.
    public class CommandCenter
    {
        //Hold all the commands.
        public List<ICommandReceiver> Commands = new List<ICommandReceiver>();

        //An argument, could be put in structs for different types to keep them cleaner.
        private string Username;

        //Where we receive the command to firstly.
        public void ReceiveCommand(string Command)
        {
            //parse commands and populate argument fields like this.Username.

            foreach (var receiver in Commands) //For all command reveivers
                if (receiver.Command == Command) //If the command is this, run the method for this class.
                    receiver.ReceiveCommand();
        }

        public void AddCommandReceiver(ICommandReceiver cr)
        {
            if (cr is IHasUsername usernameCr) usernameCr.Username = Username;

            //if(cr is AnotherParameter parameterCr)
            //{

            //Set parameter

            //}
        }
    }

    //Interface that ALL receivers implement
    public interface ICommandReceiver
    {
        string Command { get; set; }
        void ReceiveCommand();
    }

    //Interface for receivers that need a username argument
    public interface IHasUsername
    {
        string Username { get; set; }
    }

    //Implementation of a receiver
    public class ChatMessageReceiver : ICommandReceiver, IHasUsername
    {
        public string Command { get; set; } =
            "chatmessage"; //Command needs to match up with command received in the CommandCenter, then ReceiveCommand() will be fired.

        public void ReceiveCommand()
        {
            //do something with chat message
        }

        public string Username { get; set; } //Argument gets stored here by main class
    }
}