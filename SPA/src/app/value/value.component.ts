import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit {
  values: any;

  constructor(
    // public values: any,
    private http:HttpClient
    ) { }

  ngOnInit() {
    this.getValues();
  }

  // Http requests retun an observable, and to access the observable, we need to subscribe to the request.
  getValues() {
    this.http.get('http://localhost:5000/api/values')
      .subscribe( response => {
        this.values = response;
        console.log(response);
      }, error => {
        console.log(error);
      });
  }
}
