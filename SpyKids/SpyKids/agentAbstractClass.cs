using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfTrust
{
    enum Action { Start, Cheat, Cooperate }
    
    abstract class Agent
    {
        public abstract Action ChooseAction(Action lastOpponentAction);
        public Agent(Dictionary<(Action, Action), int> payoffMatrix) { }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}