using LinqToDB;
using LinqToDB.Data;
using System;
using System.Linq;

namespace Calc.Models
{
	public class DatabaseContext : DataConnection
	{
		public DatabaseContext() : base("ptocalc") { }

		public ITable<ВЗРД> ВЗРД
			=> GetTable<ВЗРД>();

		public ITable<ВКШТ> ВКШТ
			=> GetTable<ВКШТ>();

		public ITable<ГОРВБАЛАНС> ГОРВБАЛАНС
			=> GetTable<ГОРВБАЛАНС>();

		public ITable<ГОРВДОК> ГОРВДОК
			=> GetTable<ГОРВДОК>();

		public ITable<ГОРВЗАПАДНАЯ> ГОРВЗАПАДНАЯ
			=> GetTable<ГОРВЗАПАДНАЯ>();

		public ITable<ГОРВЛУЧЕСА> ГОРВЛУЧЕСА
			=> GetTable<ГОРВЛУЧЕСА>();

		public ITable<ГОРВМОНОЛИТ> ГОРВМОНОЛИТ
			=> GetTable<ГОРВМОНОЛИТ>();

		public ITable<Горвода> Горвода
			=> GetTable<Горвода>();

		public ITable<ГОРВОТЧЕТ> ГОРВОТЧЕТ
			=> GetTable<ГОРВОТЧЕТ>();

		public ITable<ГОРВЦЕНТРАЛЬНАЯ> ГОРВЦЕНТРАЛЬНАЯ
			=> GetTable<ГОРВЦЕНТРАЛЬНАЯ>();

		public ITable<ДОК> ДОК
			=> GetTable<ДОК>();

		public ITable<K1_ГАЗ> K1_ГАЗ
			=> GetTable<K1_ГАЗ>();

		public ITable<K1_МАЗУТ> K1_МАЗУТ
			=> GetTable<K1_МАЗУТ>();

		public ITable<K2_ГАЗ> K2_ГАЗ
			=> GetTable<K2_ГАЗ>();

		public ITable<K2_МАЗУТ> K2_МАЗУТ
			=> GetTable<K2_МАЗУТ>();

		public ITable<K3_ГАЗ> K3_ГАЗ
			=> GetTable<K3_ГАЗ>();

		public ITable<K3_МАЗУТ> K3_МАЗУТ
			=> GetTable<K3_МАЗУТ>();

		public ITable<K4_ГАЗ> K4_ГАЗ
			=> GetTable<K4_ГАЗ>();

		public ITable<K4_МАЗУТ> K4_МАЗУТ
			=> GetTable<K4_МАЗУТ>();

		public ITable<K5_ГАЗ> K5_ГАЗ
			=> GetTable<K5_ГАЗ>();

		public ITable<K5_МАЗУТ> K5_МАЗУТ
			=> GetTable<K5_МАЗУТ>();

		public ITable<КИМ> КИМ
			=> GetTable<КИМ>();

		public ITable<КИМ2> КИМ2
			=> GetTable<КИМ2>();

		public ITable<_KK_> KK
			=> GetTable<_KK_>();

		public ITable<КОВРЫ> КОВРЫ
			=> GetTable<КОВРЫ>();

		public ITable<НОВАЯ_ГОРВБАЛАНС> НОВАЯ_ГОРВБАЛАНС
			=> GetTable<НОВАЯ_ГОРВБАЛАНС>();

		public ITable<НОВАЯ_ГОРВДОК> НОВАЯ_ГОРВДОК
			=> GetTable<НОВАЯ_ГОРВДОК>();

		public ITable<НОВАЯ_ГОРВЗАПАДНАЯ> НОВАЯ_ГОРВЗАПАДНАЯ
			=> GetTable<НОВАЯ_ГОРВЗАПАДНАЯ>();

		public ITable<НОВАЯ_ГОРВЛУЧЕСА> НОВАЯ_ГОРВЛУЧЕСА
			=> GetTable<НОВАЯ_ГОРВЛУЧЕСА>();

		public ITable<НОВАЯ_ГОРВМОНОЛИТ> НОВАЯ_ГОРВМОНОЛИТ
			=> GetTable<НОВАЯ_ГОРВМОНОЛИТ>();

		public ITable<НОВАЯ_ГОРВОТЧЕТ> НОВАЯ_ГОРВОТЧЕТ
			=> GetTable<НОВАЯ_ГОРВОТЧЕТ>();

		public ITable<НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ> НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ
			=> GetTable<НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ>();

		public ITable<ОБЩИЙ_ОТПУСК_ТЕПЛА> ОБЩИЙ_ОТПУСК_ТЕПЛА
			=> GetTable<ОБЩИЙ_ОТПУСК_ТЕПЛА>();

		public ITable<ОБЩИЙ_ПО_КОТЛАМ_ГАЗ> ОБЩИЙ_ПО_КОТЛАМ_ГАЗ
			=> GetTable<ОБЩИЙ_ПО_КОТЛАМ_ГАЗ>();

		public ITable<ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ> ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ
			=> GetTable<ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ>();

		public ITable<ПаротрассаКИМ> ПаротрассаКИМ
			=> GetTable<ПаротрассаКИМ>();

		public ITable<Речная> Речная
			=> GetTable<Речная>();

		public ITable<РУБИКОН> РУБИКОН
			=> GetTable<РУБИКОН>();

		public ITable<СведениеБаланса> СведениеБаланса
			=> GetTable<СведениеБаланса>();

		public ITable<СодержаниеO2> СодержаниеO2
			=> GetTable<СодержаниеO2>();

		public ITable<Сырая> Сырая
			=> GetTable<Сырая>();

		public ITable<ТГ2> ТГ2
			=> GetTable<ТГ2>();

		public ITable<ТГ2_РОУ> ТГ2_РОУ
			=> GetTable<ТГ2_РОУ>();

		public ITable<Техническая> Техническая
			=> GetTable<Техническая>();

		public ITable<KA_Fuel> KA_Fuel
			=> GetTable<KA_Fuel>();

		public ITable<KA3_Aggregated> KA3_Aggregated
			=> GetTable<KA3_Aggregated>();

		public bool CreateEmptyMonth(DateTime date)
		{
			int rowsInserted = 0;

			for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
			{
				var DateRec = new DateTime(date.Year, date.Month, i);

				if (ВЗРД.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ВЗРД { DateRec = DateRec });

				if (ВКШТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ВКШТ { DateRec = DateRec });

				if (Горвода.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new Горвода { DateRec = DateRec });

				if (ДОК.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ДОК { DateRec = DateRec });

				if (K1_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K1_ГАЗ { DateRec = DateRec });

				if (K1_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K1_МАЗУТ { DateRec = DateRec });

				if (K2_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K2_ГАЗ { DateRec = DateRec });

				if (K2_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K2_МАЗУТ { DateRec = DateRec });

				if (K3_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K3_ГАЗ { DateRec = DateRec });

				if (K3_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K3_МАЗУТ { DateRec = DateRec });

				if (K4_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K4_ГАЗ { DateRec = DateRec });

				if (K4_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K4_МАЗУТ { DateRec = DateRec });

				if (K5_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K5_ГАЗ { DateRec = DateRec });

				if (K5_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new K5_МАЗУТ { DateRec = DateRec });

				if (КИМ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new КИМ { DateRec = DateRec });

				if (КИМ2.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new КИМ2 { DateRec = DateRec });

				if (KK.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new _KK_ { DateRec = DateRec });

				if (КОВРЫ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new КОВРЫ { DateRec = DateRec });

				if (НОВАЯ_ГОРВБАЛАНС.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВБАЛАНС { DateRec = DateRec });

				if (НОВАЯ_ГОРВДОК.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВДОК { DateRec = DateRec });

				if (НОВАЯ_ГОРВЗАПАДНАЯ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВЗАПАДНАЯ { DateRec = DateRec });

				if (НОВАЯ_ГОРВЛУЧЕСА.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВЛУЧЕСА { DateRec = DateRec });

				if (НОВАЯ_ГОРВМОНОЛИТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВМОНОЛИТ { DateRec = DateRec });

				if (НОВАЯ_ГОРВОТЧЕТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВОТЧЕТ { DateRec = DateRec });

				if (НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new НОВАЯ_ГОРВЦЕНТРАЛЬНАЯ { DateRec = DateRec });

				if (ОБЩИЙ_ОТПУСК_ТЕПЛА.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ОБЩИЙ_ОТПУСК_ТЕПЛА { DateRec = DateRec });

				if (ОБЩИЙ_ПО_КОТЛАМ_ГАЗ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ОБЩИЙ_ПО_КОТЛАМ_ГАЗ { DateRec = DateRec });

				if (ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ОБЩИЙ_ПО_КОТЛАМ_МАЗУТ { DateRec = DateRec });

				if (ПаротрассаКИМ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ПаротрассаКИМ { DateRec = DateRec });

				if (Речная.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new Речная { DateRec = DateRec });

				if (РУБИКОН.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new РУБИКОН { DateRec = DateRec });

				if (СведениеБаланса.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new СведениеБаланса { DateRec = DateRec });

				if (СодержаниеO2.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new СодержаниеO2 { DateRec = DateRec });

				if (Сырая.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new Сырая { DateRec = DateRec });

				if (ТГ2.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ТГ2 { DateRec = DateRec });

				if (ТГ2_РОУ.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new ТГ2_РОУ { DateRec = DateRec });

				if (Техническая.Count(x => x.DateRec == DateRec) == 0)
					rowsInserted += this.Insert(new Техническая { DateRec = DateRec });
			}

			return rowsInserted > 0;
		}
	}
}