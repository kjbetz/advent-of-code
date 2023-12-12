PuzzlePart currentPuzzlePart = PuzzlePart.Part2;
string[] lines = File.ReadAllLines("input.txt");
List<Hand> hands = new();

foreach (string line in lines)
{
    string[] handAndBet = line.Split();

    Hand hand = new(handAndBet[0], int.Parse(handAndBet[1]));

    evaluateHand(hand);

    if (currentPuzzlePart == PuzzlePart.Part2)
    {
        evaluateHandForJokers(hand);
    }

    valueHand(hand, currentPuzzlePart);

    hands.Add(hand);
}

hands = hands
    .OrderBy(h => h.Type == HandType.FiveOfAKind)
    .ThenBy(h => h.Type == HandType.FourOfAKind)
    .ThenBy(h => h.Type == HandType.FullHouse)
    .ThenBy(h => h.Type == HandType.ThreeOfAKind)
    .ThenBy(h => h.Type == HandType.TwoPair)
    .ThenBy(h => h.Type == HandType.OnePair)
    .ThenBy(h => h.Type == HandType.HighCard)
    .ThenBy(h => h.CardOne)
    .ThenBy(h => h.CardTwo)
    .ThenBy(h => h.CardThree)
    .ThenBy(h => h.CardFour)
    .ThenBy(h => h.CardFive)
    .ToList();

/*
foreach (Hand hand in hands)
{
    if (hand.Type == HandType.FullHouse && hand.Cards.Contains('J'))
    {
        Console.WriteLine($"{hand.Cards} {hand.Bet} {hand.Type}");
    }
}
*/
int result = hands.Sum(h => h.Bet * (hands.IndexOf(h) + 1));
Console.WriteLine($"Total winnings: {result}");

void evaluateHand(Hand hand)
{
    Dictionary<char, int> handEvaluation = new();
    foreach (char card in hand.Cards)
    {
        if (handEvaluation.ContainsKey(card))
        {
            handEvaluation[card]++;
        }
        else
        {
            handEvaluation.Add(card, 1);
        }
    }

    if (handEvaluation.Count == 5)
    {
        if (handEvaluation.Values.Max() == 1)
        {
            hand.Type = HandType.HighCard;
        }
        else if (handEvaluation.Values.Max() == 2)
        {
            hand.Type = HandType.OnePair;
        }
        else if (handEvaluation.Values.Max() == 3)
        {
            hand.Type = HandType.ThreeOfAKind;
        }
        else if (handEvaluation.Values.Max() == 4)
        {
            hand.Type = HandType.FourOfAKind;
        }
    }
    else if (handEvaluation.Count == 4)
    {
        hand.Type = HandType.OnePair;
    }
    else if (handEvaluation.Count == 3)
    {
        if (handEvaluation.Values.Max() == 2)
        {
            hand.Type = HandType.TwoPair;
        }
        else if (handEvaluation.Values.Max() == 3)
        {
            hand.Type = HandType.ThreeOfAKind;
        }
    }
    else if (handEvaluation.Count == 2)
    {
        if (handEvaluation.Values.Max() == 3)
        {
            hand.Type = HandType.FullHouse;
        }
        else if (handEvaluation.Values.Max() == 4)
        {
            hand.Type = HandType.FourOfAKind;
        }
    }
    else if (handEvaluation.Count == 1)
    {
        hand.Type = HandType.FiveOfAKind;
    }

    hand.HandEvaluation = handEvaluation;
}

void evaluateHandForJokers(Hand hand)
{
    if (hand.Cards.Contains('J'))
    {
        if (hand.Type == HandType.FourOfAKind)
        {
            hand.Type = HandType.FiveOfAKind;
        }
        else if (hand.Type == HandType.FullHouse)
        {
            hand.Type = HandType.FiveOfAKind;
        }
        else if (hand.Type == HandType.ThreeOfAKind)
        {
            if (hand.HandEvaluation.Count == 2)
            {
                hand.Type = HandType.FiveOfAKind;
            }
            else
            {
                hand.Type = HandType.FourOfAKind;
            }
        }
        else if (hand.Type == HandType.TwoPair)
        {
            if (hand.HandEvaluation.Count == 3)
            {
                if (hand.HandEvaluation['J'] == 2)
                {
                    hand.Type = HandType.FourOfAKind;
                }
                else
                {
                    hand.Type = HandType.FullHouse;
                }
            }
            else
            {
                hand.Type = HandType.ThreeOfAKind;
            }
        }
        else if (hand.Type == HandType.OnePair)
        {
            hand.Type = HandType.ThreeOfAKind;
        }
        else if (hand.Type == HandType.HighCard)
        {
            hand.Type = HandType.OnePair;
        }
    }

    return;
}

void valueHand(Hand hand, PuzzlePart puzzlePart)
{
    hand.CardValues = new int[5];
    int i = 0;
    foreach (char card in hand.Cards)
    {
        switch (card)
        {
            case 'A':
                hand.CardValues[i] = 14;
                break;
            case 'K':
                hand.CardValues[i] = 13;
                break;
            case 'Q':
                hand.CardValues[i] = 12;
                break;
            case 'J':
                // Jack is 11 in Part 1; Joker is 1 in Part 2
                hand.CardValues[i] = puzzlePart == PuzzlePart.Part1 ? 11 : 1;
                break;
            case 'T':
                hand.CardValues[i] = 10;
                break;
            default:
                hand.CardValues[i] = int.Parse(card.ToString());
                break;
        }
        i++;
    }

    hand.CardOne = hand.CardValues[0];
    hand.CardTwo = hand.CardValues[1];
    hand.CardThree = hand.CardValues[2];
    hand.CardFour = hand.CardValues[3];
    hand.CardFive = hand.CardValues[4];
}

enum PuzzlePart
{
    Part1,
    Part2
}

enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}

class Hand
{
    public HandType Type { get; set; }
    public int CardOne { get; set; }
    public int CardTwo { get; set; }
    public int CardThree { get; set; }
    public int CardFour { get; set; }
    public int CardFive { get; set; }
    public string Cards { get; set; }
    public int[] CardValues { get; set; }
    public Dictionary<char, int> HandEvaluation { get; set; }
    public int Bet { get; set; }

    public Hand(string cards, int bet)
    {
        Cards = cards;
        Bet = bet;
    }
}
