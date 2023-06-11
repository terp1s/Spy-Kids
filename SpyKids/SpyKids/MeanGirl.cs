using System.Collections.Generic;

namespace GameOfTrust
{
    class MeanGirl : Agent
    {
        public override Action ChooseAction(Action lastOpponentAction)
        {
           
            return Action.Cheat;
        }

        public MeanGirl(Dictionary<(Action, Action), int> payoffMatrix) : base(payoffMatrix)
        {

        }
    }
}