using System.Collections.Generic;

namespace GameOfTrust
{
    class NaiveAgent : Agent
    {
        public override Action ChooseAction(Action lastOpponentAction)
        {
            return Action.Cooperate;
        }

        public NaiveAgent(Dictionary<(Action, Action), int> payoffMatrix) : base(payoffMatrix)
        {

        }
    }
}