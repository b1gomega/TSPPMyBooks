using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBooks {
    class Login {
        private string login = "admin";
        private string password = "admin";
        private bool isAutorization = false;
        //Перевірка чи авторизований користувач
        public bool CheckAutorization() {   
             return isAutorization;
        }
        //Змінити статус авторизації
        public void EditStatusAutorization(bool status) {
            if (status == false) {
                isAutorization = false;
            }
            else {
                isAutorization = true;
            }
        }

        //Перевірка на правильність введення логіна та паролю
        public bool CheckLoginAndPassword(string _login, string _password) {
            _login = _login.Trim();
            if (login == _login && password == _password) {
                isAutorization = true;
            }
            return isAutorization;
        }
    }
}
