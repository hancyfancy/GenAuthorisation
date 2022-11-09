import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserToken } from '../../models/user-token';
import { AuthorisationService } from '../../services/authorisation.service';
import { Settings } from '../../settings';

@Component({
  selector: 'app-validate',
  templateUrl: './validate.component.html',
  styleUrls: ['./validate.component.css']
})
export class ValidateComponent implements OnInit {
  protected userToken: UserToken = new UserToken();

  constructor(private authorisationService: AuthorisationService, private settings: Settings, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.userToken = new UserToken();
  }

  validate(token: string): void {
    this.authorisationService.validateUser(new UserToken(0n, token))
      .then((jwtToken) => {
        console.log(jwtToken!);
      })
      .catch((error) => {
        console.log("Promise rejected with " + JSON.stringify(error));
      });
  }
}
