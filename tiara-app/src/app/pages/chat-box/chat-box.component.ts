import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.scss']
})
export class ChatBoxComponent implements OnInit {
  messages : any[] = [];
  personTyping: string;

  constructor(private http: HttpService) { }

  ngOnInit() {
    let person = localStorage.getItem("person");
    if(person == "Boy"){
      this.personTyping = "@manesan.pillay"
    }
    else{
      this.personTyping = "@kiashka.28"
    }


    this.http.get(`messages/getall`).subscribe((result) => {
      console.log(result)
      this.messages = result;
    })
  }

  sendMessage(event: any,  reply: boolean) {
    const files = !event.files ? [] : event.files.map((file) => {
      return {
        url: file.src,
        type: file.type,
        icon: 'file-text-outline',
      };
    });

    this.messages.push({
      description: event.message,
      createdDate: new Date(),
      reply: reply,
      type: files.length ? 'file' : 'text',
      files: files,
      createdBy: {
        userName: this.personTyping,
      },
    });

    this.http.post(`messages/createmessage`, this.messages[this.messages.length-1]).subscribe((result) => {
      console.log(result);
    })
  }
}
