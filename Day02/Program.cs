using Day02;
using System.Diagnostics;

List<string> input = File.ReadAllLines("./input.txt").ToList();
List<Round> rounds = new List<Round>();

foreach (var item in input)
{
    char a = item[0];
    char b = item[2];
    Round round = new Round(a, b);
    rounds.Add(round);
}

int partOneScore = SolvePartOne(rounds);
int partTwoScore = SolvePartTwo(rounds);
Console.WriteLine($"Part One: {partOneScore}\n" +
    $"Part Two: {partTwoScore}");


int SolvePartOne(List<Round> rounds)
{
    int score = 0;

    foreach (var round in rounds)
    {
        //Adds score for rock, paper or sissor
        switch (round.Me)
        {
            case 'X':
                score += 1;
                break;
            case 'Y':
                score += 2;
                break;
            case 'Z':
                score += 3;
                break;
            default:
                break;
        }
        //Adds score for win, draw or loss
        switch (round.Opponent)
        {
            case 'A':
                if(round.Me == 'X')
                {
                    score += 3;
                }else if(round.Me == 'Y')
                {
                    score += 6;
                    
                }else if(round.Me == 'Z')
                {
                    score += 0;
                }
                break;
            case 'B':
                if (round.Me == 'X')
                {
                    score += 0;
                }
                else if (round.Me == 'Y')
                {
                    score += 3;

                }
                else if (round.Me == 'Z')
                {
                    score += 6;
                }
                break;
            case 'C':
                if (round.Me == 'X')
                {
                    score += 6;
                }
                else if (round.Me == 'Y')
                {
                    score += 0;

                }
                else if (round.Me == 'Z')
                {
                    score += 3;
                }
                break;
        }
    }
    return score;
}

int SolvePartTwo(List<Round> rounds)
{
    int score = 0;

    foreach (Round round in rounds)
    {
        //X = Loss      A = Rock
        //Y = Draw      B = Paper
        //Z = Win       C = Sissor
        char opponent = round.Opponent;
        char me = 'A';
        switch (round.Me)
        {
            case 'X':
                if(opponent == 'A')
                {
                    me = 'C';
                }else if(opponent == 'B')
                {
                    me = 'A';
                }else if(opponent == 'C')
                {
                    me = 'B';
                }
                break;
            case 'Y':
                me = opponent;
                score += 3;
                break;
            case 'Z':
                if(opponent == 'A')
                {
                    me = 'B';
                }else if(opponent == 'B')
                {
                    me = 'C';
                }else if(opponent == 'C')
                {
                    me = 'A';
                }
                score += 6;
                break;
        }
        switch (me)
        {
            case 'A':
                score += 1;
                break;
            case 'B':
                score += 2;
                break;
            case 'C':
                score += 3;
                break;
        }
    }

    return score;
}