import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'hello';
  users: any;
  constructor(
    private httpClient: HttpClient,
    private accountService: AccountService
  ) {}
  ngOnInit(): void {
    this.getUser();
    this.setCurrentUser();
  }
  getUser() {
    this.httpClient.get('http://localhost:5001/api/users').subscribe({
      next: (res) => (this.users = res),
      error: (err) => console.log(err),
      complete: () => console.log('Request has completed'),
    });
  }
  setCurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }
}
