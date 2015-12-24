using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Selectors
{
    public enum RolesEnum
    {
        ReadOnly = 0,
        IllegalEdit = 1,
        SupplierEditOwn = 2,
        SupplierEditAll = 3,
        ChiefEditOwn = 4,
        ChiefEditAll = 5,
        Admin = 6,
        OnlyBillboards = 7,
    }

    public class RolesList
    {
        public static bool IsInRole(RolesEnum roles, IPrincipal user)
        {
            return user.IsInRole(Roles[(int)roles].StoredValue);
        }

        private static List<RoleItem> _roles = null;
        public static List<RoleItem> Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new List<RoleItem> {
                        new  RoleItem{
                            DisplayValue = "Только чтение",
                            StoredValue = "ReadOnly"},
                        new RoleItem{
                            DisplayValue = "Неопознанные конструкции",
                            StoredValue = "IllegalEdit"
                        },
                        new RoleItem{
                            DisplayValue = "Сотрудник",
                            StoredValue = "SupplierEditOwn"
                        },
                        new RoleItem{
                            DisplayValue = "Сотрудник S",
                            StoredValue = "SupplierEditAll"
                        },
                        new RoleItem{
                            DisplayValue = "Начальник",
                            StoredValue = "ChiefEditOwn"
                        },
                        new RoleItem{
                            DisplayValue = "Начальник S",
                            StoredValue = "ChiefEditAll"
                        },
                        new RoleItem{
                            DisplayValue = "Администратор",
                            StoredValue = "Admin"
                        },
                        new RoleItem{
                            DisplayValue = "Только билборды",
                            StoredValue = "OnlyBillboards"
                        }
                    };
                }
                return _roles;
            }
        }
    }
}
