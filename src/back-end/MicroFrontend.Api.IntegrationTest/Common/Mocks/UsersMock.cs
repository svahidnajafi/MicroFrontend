using MicroFrontend.Api.Domain.Entities;

namespace MicroFrontend.Api.IntegrationTest.Mocks;

public static class UsersMock
{
    public static List<User> Users = new()
    {
        new() { Id = "U1", Name = "Vahid", Email = "Vahidnajafi.work@gmail.com" },
        new() { Id = "U2", Name = "Andreas", Email = "plange@brenner.de" },
        new() { Id = "U3", Name = "Emilie", Email = "yfreitag@freenet.de" },
        new() { Id = "U4", Name = "Johanna", Email = "angela80@wagner.de" },
        new() { Id = "U5", Name = "Alberta", Email = "khentschel@t-online.de" }
    };
}