namespace DevFreela.API.Entities
{
    public class UserSkill : BaseEntity
    {
        public UserSkill(int idUser, int idSkill) : base()
        {
            ID_User = idUser;
            ID_Skill = idSkill;
        }

        public int ID_User { get; private set; }
        public int ID_Skill { get; private set; }
        public User User { get; private set; }
        public Skill Skill { get; private set; }
    }
}
