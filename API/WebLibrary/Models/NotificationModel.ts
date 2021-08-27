module App { 
    export class NotificationModel { 
        public sender: string;
        public recipient: string;
        public subject: string;
        public content: string;
        public isRead: boolean;
        public received: Date;
        public type: NotificationType;
    }
}