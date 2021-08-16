import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NbToastrService } from '@nebular/theme';
import { HttpService } from 'src/environments/http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  dataModel: any = {};
  name: any;

  constructor(private http: HttpService,
    private toaster: NbToastrService,
    private route: ActivatedRoute,
    private router: Router
    ) {}

  ngOnInit() {
    if(("api" in localStorage && "person" in localStorage)){
      this.router.navigate(['dashboard'], { relativeTo: this.route });
    }
  }

  authenticate() {
    this.http.post(`users/authenticate`, this.dataModel).subscribe(
      (result) => {
        localStorage.setItem('api', JSON.stringify(result.token))
        localStorage.setItem('person', result.role)
        this.router.navigate(['dashboard'], { relativeTo: this.route });
      },
      (error) => {
        this.toaster.warning(error.error, "Oops!")
      }
    );
  }
}
