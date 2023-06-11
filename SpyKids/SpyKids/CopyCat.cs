using System.Collections.Generic;

namespace GameOfTrust
{
    class CopyCat : Agent
    {
        public override Action ChooseAction(Action lastOpponentAction)
        {
            if(lastOpponentAction == Action.Start)
            {
                return Action.Cooperate;
            }
            return lastOpponentAction;
        }

        public CopyCat(Dictionary<(Action, Action), int> payoffMatrix) : base(payoffMatrix)
        {

        }
    }
}