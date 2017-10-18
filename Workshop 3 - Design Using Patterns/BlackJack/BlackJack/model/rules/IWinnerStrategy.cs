using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    interface IWinnerStrategy
    {
        bool IsDealerWinner(model.Player a_player, model.Player a_dealer);
    }
}