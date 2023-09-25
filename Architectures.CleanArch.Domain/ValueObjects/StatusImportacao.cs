namespace Architectures.CleanArch.Domain.ValueObjects;

public enum Status
{
    PROCESSANDO,
    CONCLUIDO,
    ERRO
}

public class StatusImportacao
{
    public Status Status { get; set; }

    public StatusImportacao(Status status)
    {
        Status = status;
    }
}
