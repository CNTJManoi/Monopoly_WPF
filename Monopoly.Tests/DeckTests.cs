using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Monopoly.Logic;
using Monopoly.Logic.Cards;
using Xunit;

namespace Monopoly.Tests
{
    public class DeckTests
    {
        private Configuration _configuration;

        public DeckTests()
        {
            Game g = new Game(2);
            _configuration = new Configuration(g);
        }

        [Fact]
        public void Push_AddsCardToTopOfDeck()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);
            var card = cards[0];
            // Act
            deck.Pop();
            deck.Push(card);

            // Assert
            Assert.Equal(card, deck.Peek());
        }

        [Fact]
        public void Peek_ReturnsTopCard()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);
            var card = deck.Cards[18];

            // Act
            var topCard = deck.Peek();

            // Assert
            Assert.Equal(card, topCard);
        }

        [Fact]
        public void Pop_RemovesAndReturnsTopCard()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);
            var card1 = deck.Cards[18];
            var card2 = deck.Cards[17];

            // Act
            var topCard = deck.Pop();

            // Assert
            Assert.Equal(card1, topCard);
            Assert.Equal(card2, deck.Peek());
        }

        [Fact]
        public void IsEmpty_ReturnsTrueWhenTopEqualsMinusOne()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);

            // Act
            while (!deck.IsEmpty())
            {
                deck.Pop();
            }

            // Assert
            Assert.True(deck.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ResetsDeckToOriginalCardsWhenTopEqualsMinusOne()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);

            // Act
            while (!deck.IsEmpty())
            {
                deck.Pop();
            }

            deck.Peek();
            // Assert
            Assert.Equal(deck.Cards[18], deck.Peek());
            Assert.Equal(deck.Cards[18], deck.Pop());
        }

        [Fact]
        public void Shuffle_RearrangesCards()
        {
            // Arrange
            var cards = _configuration.GetAllCards(@"Config\CardDescriptions").ToList();
            var deck = new Deck(cards);
            var card1 = cards[0];

            // Act
            deck.Shuffle();

            // Assert
            Assert.NotEqual(card1, deck.Peek());
        }
    }

}