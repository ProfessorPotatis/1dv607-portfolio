using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class DealerWinsOnEqualWinnerStrategy : IWinnerStrategy
    {
        private const int g_maxScore = 21;

        public bool IsDealerWinner(model.Player a_player, model.Player a_dealer)
        {
            if (a_player.CalcScore() > g_maxScore)
            {
                return true;
            }
            else if (a_dealer.CalcScore() > g_maxScore)
            {
                return false;
            }
            return a_dealer.CalcScore() >= a_player.CalcScore();
        }
    }
}