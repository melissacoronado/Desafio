using ApiMutants.Application.Interfaces;
using ApiMutants.Domain.Config;
using ApiMutants.Domain.NonEntities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApiMutants.Services;

public class MutantsService : IMutantsService
{
    private readonly IOptions<SequenceConfig> _optionsConfig;
    private readonly ILogger<MutantsService> _logger;

    public MutantsService(IOptions<SequenceConfig> optionsConfig,
        ILogger<MutantsService> logger)
    {
        _optionsConfig = optionsConfig;
        _logger = logger;
    }

    public bool isMutant(Mutants data)
    {
        if (data.DNA == null)
            throw new Exception("Lista No contiene ninguna secuencia.");

        if (data.DNA.Count() <= _optionsConfig.Value.MinQuantity)

            throw new Exception("Lista No contiene la cantidad minima de secuencias.");

        char[,] matrix = CovertMatrix(data.DNA);

        int totalRepetions =
            CheckHorizontally(matrix, data.DNA.Count()) +
            CheckVertically(matrix, data.DNA.Count()) +
            CheckDiagonallyLeftRight(matrix, data.DNA.Count()) +
            CheckDiagonallyRightLeft(matrix, data.DNA.Count());

        return totalRepetions > 1;
    }

    private char[,] CovertMatrix(List<string> data)
    {
        int lengthData = data.Count();
        char[,] matrix = new char[lengthData, lengthData];

        for (int i = 0; i < lengthData; i++)
        {
            for (int j = 0; j < lengthData; j++)
            {
                matrix[i, j] = data[i][j];
            }
        }

        return matrix;
    }

    private int CheckHorizontally(char[,] matrix, int lengthData)
    {
        int sequenceQuantity = 0;

        for (int i = 0; i < lengthData; i++)
        {
            for (int j = 0; j <= lengthData - 4; j++)
            {
                if (matrix[i, j] == matrix[i, j + 1] && matrix[i, j] == matrix[i, j + 2] &&
                    matrix[i, j] == matrix[i, j + 3])
                {
                    sequenceQuantity++;
                }
            }
        }

        return sequenceQuantity;
    }

    private int CheckVertically(char[,] matrix, int lengthData)
    {
        int sequenceQuantity = 0;

        for (int i = 0; i < lengthData; i++)
        {
            for (int j = 0; j <= lengthData - 4; j++)
            {
                if (matrix[j, i] == matrix[j + 1, i] && matrix[j, i] == matrix[j + 2, i] &&
                    matrix[j, i] == matrix[j + 3, i])
                {
                    sequenceQuantity++;
                }
            }
        }

        return sequenceQuantity;
    }

    private int CheckDiagonallyLeftRight(char[,] matrix, int lengthData)
    {
        int sequenceQuantity = 0;

        for (int i = 0; i < lengthData - 4; i++)
        {
            for (int j = 0; j <= lengthData - 4; j++)
            {
                if (matrix[i, j] == matrix[i + 1, j + 1] &&
                    matrix[i, j] == matrix[i + 2, j + 2] &&
                    matrix[i, j] == matrix[i + 3, j + 3])
                {
                    sequenceQuantity++;
                }
            }
        }

        return sequenceQuantity;
    }

    private int CheckDiagonallyRightLeft(char[,] matrix, int lengthData)
    {
        int sequenceQuantity = 0;

        for (int i = lengthData - 1; i > lengthData - 4; i--)
        {
            for (int j = lengthData - 1; j >= lengthData - 4; j--)
            {
                if (matrix[i, j] == matrix[i - 1, j - 1] &&
                    matrix[i, j] == matrix[i - 2, j - 2] &&
                    matrix[i, j] == matrix[i - 3, j - 3])
                {
                    sequenceQuantity++;
                }
            }
        }

        return sequenceQuantity;
    }

}
