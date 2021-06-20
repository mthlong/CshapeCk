using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEF.DAO
{
    public class UserDAO
    {
        NguyenHoangLongContext db = null;

        public UserDAO()
        {
            db = new NguyenHoangLongContext();
        }
        //Đăng nhập
        public int Login(String UserName, String UserPassword)
        {
            var result = db.UserAccounts.SingleOrDefault(x => x.UserName == UserName);
            if (result == null) //Nếu không tồn tại username thì return 0
            {
                return 0;
            }
            else
            {
                if (result.Status == false) //Trả về -1 nếu status của tài khoản đang ở trạng thái false
                {
                    return -1;
                }
                else
                {
                    if (result.Password == UserPassword)
                    {
                        return 1; //Trả về 1 nếu tên đăng nhập và mật khẩu đều đúng
                    }
                    else
                    {
                        return -2; //Trả về -2 nếu đúng tên đăng nhập nhưng sai mật khẩu
                    }
                }

            }
        }
        public UserAccount getUserByID(string user)
        {
            return db.UserAccounts.SingleOrDefault(x => x.UserName.Contains(user));
        }
    }
}
