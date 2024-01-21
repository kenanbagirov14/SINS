namespace NIS.UtilsCore.Enums
{
    public enum RequestStatusEnum
    {
        Yeni = 1,
        Icrada,
        Bağlanılıb,
        SəhvMuraciet,
        IcraOlundu
    }

    public enum TaskStatusEnum
    {
        Yeni = 1,
        Icrada,
        IcraOlundu,
        ImtinaEdildi,
        Yonlendirildi,
        Qaytarilib,
        YenidenIcraya
    }

    public enum FileType
    {
        Request = 1,
        Task,
        Comment
    }


}
