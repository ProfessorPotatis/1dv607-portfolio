using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame
    {
        private model.Game m_game;
        private view.IView m_view;

        public PlayGame(model.Game a_game, view.IView a_view)
        {
            this.m_game = a_game;
            this.m_view = a_view;
        }

        public bool Play()
        {
            m_view.DisplayWelcomeMessage();
            
            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());

            if (m_game.IsGameOver())
            {
                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            view.Event e = m_view.GetInput();

            if (e == view.Event.Play)
            {
                m_game.NewGame();
            }
            else if (e == view.Event.Hit)
            {
                m_game.Hit();
            }
            else if (e == view.Event.Stand)
            {
                m_game.Stand();
            }

            return e != view.Event.Quit;
        }
    }
}
