module App { 
    export class CreateUserModel { 
        public username: string;
        public password: string;
        public confirmPassword: string;
        public role: string;
        public info: UserInfoModel;
    }
}