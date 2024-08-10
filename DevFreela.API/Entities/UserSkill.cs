namespace DevFreela.API.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int idUser, int idSkill) : base()
        {
            ID_User = idUser;
            ID_Skill = idSkill;
        }

        public int ID_User { get; set; }
        public int ID_Skill { get; set; }
        public User User { get; set; }
        public Skill Skill { get; set; }
    }
}
