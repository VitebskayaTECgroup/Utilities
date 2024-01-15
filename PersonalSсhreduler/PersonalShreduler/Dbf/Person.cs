using System;

namespace PersonalShreduler.Dbf
{
	public class Person
	{
        public int TabId { get; set; }

        public string Family { get; set; }

        public string Guild { get; set; }

        public DateTime BirthDate { get; set; }

        public void Sanitaze()
        {
            Family = Family.Trim();
            Guild = Guild.Trim();

            string s = "";
            char[] chars = Family.ToLower().ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 0)
                {
                    s += chars[i].ToString().ToUpper();
                }
                else if (chars[i - 1] == ' ' || chars[i - 1] == '-')
                {
                    s += chars[i].ToString().ToUpper();
                }
                else
                {
                    s += chars[i].ToString();
                }
            }

            Family = s;
        }
    }
}