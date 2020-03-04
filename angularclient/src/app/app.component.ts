import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  time: string;
  url: 'http://localhost:1337/time';
  constructor(private client: HttpClient){}

  updateTime() {
    this.client.get<{data: string}>(this.url)
    .subscribe(r => {
      console.log(r.data);
      this.time = r.data;
    });
  }

  changeUrl(){
    this.url = 'http://localhost:1337/time';
  }
}
