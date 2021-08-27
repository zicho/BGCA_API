module App { 
    export class PrivateMessageModel { 
        public sender: string;
        public recipient: string;
        public subject: string;
        public content: string;
        public isRead: boolean;
    }
}