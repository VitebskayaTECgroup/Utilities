using LinqToDB;
using System.Linq;

namespace Checkpoint.Models
{
	public static class DatabaseContextExtentions
	{
		public static Operator Authentication(this DatabaseContext db, string winName)
		{
			// идентифицируем пользователя по имени его учетной записи
			winName = winName
				.ToLower()
				.Replace("vst\\", "");

			if ("esmal asha yuri tai_eng".Contains(winName)) winName = "void";

			var query = from op in db.OPERATORS
						from ow in db.OWNERS.InnerJoin(x => x.OW_ID == op.OP_OWNER)
						where op.OP_LOGIN == winName
						select new Operator
						{
							Id = ow.OW_ID,
							TableId = ow.OW_TABNUM,
							Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
							Description = op.OP_DESC,
							DepartmentId = ow.OW_DEP,
							IsCRUD = "void crow sova esmal".Contains(winName)
						};

			return query.FirstOrDefault();
		}
	}
}