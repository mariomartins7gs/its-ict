namespace Atleti;

public interface IAtletaUniversale : IAtleta, ITennista, INuotatore
{
    string Mangio();
    string Bevo();
}
