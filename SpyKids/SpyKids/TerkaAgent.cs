using System;
using System.Collections.Generic;


namespace GameOfTrust
{
    class TerkaAgent : Agent
    {
        enum Opponent { Coop, Cheat, Grudger, Detective, Copycat, PC, Idk, Coopdgercat }


        private Opponent Opp = Opponent.Idk;

        private int RoundCount = 0;
        private Dictionary<(Action, Action), int> PayoffMatrix;
        private int CheatOrCoop;
        private int Cheat;
        private int Coop;
        private int CopycatMax;
        private int CoopMax;
        private int CheatMax;
        private int DetMax;
        private Action MyLastAction;

        private (Action, bool) Coopdgercat(Action lastOpponentAction)
        {
            if(RoundCount == 3)
            {
                if (54 * PayoffMatrix[(Action.Cooperate, Action.Cooperate)] > (18 * (CheatMax + CoopMax) + 9 * CopycatMax))
                {
                    Opp = Opponent.Coopdgercat;

                    return (Action.Cooperate, false);
                }
                else
                {
                    return (Action.Cheat, true);
                }
            }

            if(RoundCount == 4)
            {
                return (Action.Cooperate, true);
            }
            
            if(RoundCount == 5)
            {
                if(lastOpponentAction == Action.Cooperate)
                {
                    Opp = Opponent.Coop;

                    return (Action.Cooperate, false);
                }
                else
                {
                    if(PayoffMatrix[(Action.Cheat, Action.Cooperate)] + PayoffMatrix[(Action.Cheat, Action.Cheat)] > PayoffMatrix[(Action.Cooperate, Action.Cheat)] + PayoffMatrix[(Action.Cooperate, Action.Cooperate)])
                    {
                        MyLastAction = Action.Cheat;
                        return (Action.Cheat, true);
                    }
                    else
                    {
                        MyLastAction = Action.Cooperate;
                        return (Action.Cooperate, true);
                    }
                }

            }

            if(RoundCount == 6)
            {
                if(lastOpponentAction == Action.Cooperate)
                {
                    Opp = Opponent.Copycat;

                    return (Action.Cooperate, false);
                }
                else
                {
                    Opp = Opponent.Grudger;

                    return (Action.Cooperate, false);
                }
            }
            else
            {
                
                 Opp = Opponent.PC;
                
                return (Action.Cooperate, false);
            }
            
        }
        private (Action, bool) DisOpponent(Action lastOpponentAction)
        {
            if(RoundCount == 1)
            {
                return (Action.Cooperate, true);
            }
            else if (RoundCount == 2)
            {
                if(lastOpponentAction == Action.Cheat)
                {
                    Opp = Opponent.Cheat;

                    return (Action.Cooperate, false);
                }
                else
                {
                    return (Action.Cooperate, true);
                }
               
            }
            else if (RoundCount == 3)
            {
                if(lastOpponentAction == Action.Cheat)
                {
                    Opp = Opponent.Detective;

                    return (Action.Cooperate, false);
                }
                else
                {
                    return Coopdgercat(lastOpponentAction);

                }
            }
            else
            {
                return Coopdgercat(lastOpponentAction);

            }
        }

        public override Action ChooseAction(Action lastOpponentAction)
        {
            RoundCount += 1;
            
            if (Opp == Opponent.Idk)
            {
                DisOpponent(lastOpponentAction);

            }

            if (Opp == Opponent.Cheat)
            {
                if(lastOpponentAction == Action.Cooperate)
                {
                    Opp = Opponent.PC;

                }

                if(CheatMax ==  PayoffMatrix[(Action.Cheat, Action.Cheat)])
                {
                    return Action.Cheat;
                }
                else
                {
                    return Action.Cooperate;
                }
            }
            else if (Opp == Opponent.Coop)
            {
                if (lastOpponentAction == Action.Cheat)
                {
                    Opp = Opponent.PC;

                }

                if (CoopMax == PayoffMatrix[(Action.Cheat, Action.Cooperate)])
                {
                    return Action.Cheat;
                }
                else
                {
                    return Action.Cooperate;
                }
            }
            else if (Opp == Opponent.Copycat)
            {
                if (lastOpponentAction != MyLastAction)
                {
                    Opp = Opponent.PC;

                }

                if (CopycatMax == PayoffMatrix[(Action.Cooperate, Action.Cooperate)])
                {
                    MyLastAction = Action.Cooperate;
                    return MyLastAction;

                }
                else if(CopycatMax == PayoffMatrix[(Action.Cheat, Action.Cheat)])
                {
                    MyLastAction = Action.Cheat;
                    return MyLastAction;
                }
                else
                {
                    if(lastOpponentAction == Action.Cheat)
                    {
                        MyLastAction = Action.Cooperate;
                        return MyLastAction;
                    }
                    else
                    {
                        MyLastAction = Action.Cheat;
                        return MyLastAction;
                    }
                }
            }
            else if (Opp == Opponent.Coopdgercat)
            {
                return Action.Cooperate;
            }
            else if(Opp == Opponent.Detective)
            {
                if(RoundCount == 3)
                {
                    if(DetMax == CopycatMax)
                    {
                        Opp = Opponent.Copycat;

                        return Action.Cheat;
                    }
                    else
                    {
                        return Action.Cooperate;
                    }
                }

                if(RoundCount == 4)
                {
                    return Action.Cooperate;
                }

                if(PayoffMatrix[(Action.Cooperate, Action.Cheat)] > PayoffMatrix[(Action.Cheat, Action.Cheat)])
                {
                    return Action.Cooperate;

                }
                else
                {
                    return Action.Cheat;
                }

            }
            else if(Opp == Opponent.Grudger)
            {
                if(CheatMax == PayoffMatrix[(Action.Cooperate, Action.Cheat)])
                {
                    return Action.Cooperate;
                }
                else
                {
                    return Action.Cheat;
                }
            }

            if (Opp == Opponent.PC)
            {
                Random r = new Random();
                int rInt = r.Next(0, 1);

                if (rInt < CheatOrCoop)
                {
                    return Action.Cheat;
                }
                else
                {
                    return Action.Cooperate;
                }
            }

            return Action.Cooperate;
        }

        public TerkaAgent(Dictionary<(Action, Action), int> payoffMatrix) : base(payoffMatrix)
        {
            PayoffMatrix = payoffMatrix;
            CopycatMax = System.Math.Max(2 * PayoffMatrix[(Action.Cooperate, Action.Cooperate)], System.Math.Max(2 * PayoffMatrix[(Action.Cheat, Action.Cheat)], PayoffMatrix[(Action.Cooperate, Action.Cheat)] + PayoffMatrix[(Action.Cheat, Action.Cooperate)]));
            CoopMax = System.Math.Max(PayoffMatrix[(Action.Cooperate, Action.Cooperate)], PayoffMatrix[(Action.Cheat, Action.Cooperate)]);
            CheatMax = System.Math.Max(PayoffMatrix[(Action.Cooperate, Action.Cheat)], PayoffMatrix[(Action.Cheat, Action.Cheat)]);
            DetMax = System.Math.Max(CopycatMax, 2 * PayoffMatrix[(Action.Cooperate, Action.Cheat)]);
            Cheat = (PayoffMatrix[(Action.Cheat, Action.Cooperate)] + PayoffMatrix[(Action.Cheat, Action.Cheat)]) / 2;
            Coop = (PayoffMatrix[(Action.Cooperate, Action.Cooperate)] + PayoffMatrix[(Action.Cooperate, Action.Cheat)]) / 2;
            CheatOrCoop = Cheat / (Cheat + Coop);
        }
    }
}