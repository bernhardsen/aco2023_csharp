namespace Day10;

public class PipeLayer
{
    private readonly List<char[]> _pipeMap;
    private readonly List<Position> _loop = new ();
    
    public PipeLayer(string filePath)
    {
        _pipeMap = File.ReadAllLines(filePath).Select(line => line.ToCharArray()).ToList();
        BuildLoop();
    }

    public int MaxDistanceInLoop()
    {
        return _loop.Count / 2;
    }

    public int CountTilesInsideLoop()
    {
        int contained = 0;

        for (var y = 0; y < _pipeMap.Count; y++)
        {
            var isInside = false;
            var isOnPipe = false;
            var lastCorner = '|';
            for (var x = 0; x < _pipeMap[y].Length; x++)
            {
                if (_loop.Contains(new Position(x, y)))
                {
                    if (isOnPipe)
                    {
                        if (_pipeMap[y][x] == 'J')
                        {
                            if (lastCorner == 'F')
                            {
                                isInside = !isInside;
                            }
                            isOnPipe = false;
                        }
                        else if (_pipeMap[y][x] == '7')
                        {
                            if (lastCorner == 'L')
                            {
                                isInside = !isInside;
                            }
                            isOnPipe = false;
                        }
                    }
                    else if (_pipeMap[y][x] == '|')
                    {
                        isInside = !isInside;
                    }
                    else
                    {
                        isOnPipe = true;
                        lastCorner = _pipeMap[y][x];
                    }
                }
                else if (isInside)
                {
                    contained++;
                }
            }
        }

        return contained;
    }

    private void BuildLoop()
    {
        Position start = GetStartPosition();
        var x = start.X;
        var y = start.Y;
        var prevX = x;
        var prevY = y;
        while (_loop.Count == 0 || x != start.X || y != start.Y)
        {
            _loop.Add(new Position(x, y));
            var nextPrevX = x;
            var nextPrevY = y;
            switch (_pipeMap[y][x])
            {
                case '|':
                    if (prevY < y) y += 1;
                    else y -= 1;
                    break;
                case '-':
                    if (prevX < x) x += 1;
                    else x -= 1;
                    break;
                case 'L':
                    if (prevY < y) x += 1;
                    else y -= 1;
                    break;
                case 'J':
                    if (prevY < y) x -= 1;
                    else y -= 1;
                    break;
                case '7':
                    if (prevY > y) x -= 1;
                    else y += 1;
                    break;
                case 'F':
                    if (prevY > y) x += 1;
                    else y += 1;
                    break;
                default:
                    if (_pipeMap[y + 1][x] == '|' || _pipeMap[y + 1][x] == 'J' || _pipeMap[y + 1][x] == 'L') y += 1;
                    else if (_pipeMap[y][x + 1] == '-' || _pipeMap[y][x + 1] == 'J' || _pipeMap[y][x + 1] == '7') x += 1;
                    else y -= 1;
                    break;
            }

            prevX = nextPrevX;
            prevY = nextPrevY;
        }
    }

    private Position GetStartPosition()
    {
        for (var y = 0; y < _pipeMap.Count; y++)
        {
            for (var x = 0; x < _pipeMap[y].Length; x++)
            {
                if (_pipeMap[y][x] == 'S')
                {
                    return new Position(x, y);
                }
            }
        }

        return new Position(0, 0);
    }
}

internal record Position(int X, int Y);
