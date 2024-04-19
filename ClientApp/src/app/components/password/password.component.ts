import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from 'src/app/services/authentication.service';
import { User } from 'src/app/models/user';
import { ToastrService } from 'ngx-toastr';

@Component({ templateUrl: 'password.component.html' })
export class PasswordComponent implements OnInit {
    passwordForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    hideOldPassword = true;
    hideNewPassword = true;
    hideNewPassword2 = true;
    currentUser: any;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private toastr: ToastrService,
    ) { 
        this.authenticationService.currentUser.subscribe(
            x => (this.currentUser = x)
          );
    }

    ngOnInit() {
        this.passwordForm = this.formBuilder.group({
            oldPassword: ['', Validators.required],
            newPassword: ['', Validators.required],
            newPassword2: ['', Validators.required]
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.passwordForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.passwordForm.invalid) {
            return;
        }

        this.loading = true;

        // Establish user credentials
        let oldUserCred = {
            //username: this.f.username.value,
            username: this.currentUser.userName,
            password: this.f.oldPassword.value
        };
        let newUserCred = {
            //username: this.f.username.value,
            username: this.currentUser.userName,
            password: this.f.newPassword.value
        };

        this.authenticationService.changePassword(oldUserCred, newUserCred)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate([this.returnUrl]);
                    this.showSuccess();
                },
                error => {
                    this.error = error;
                    this.loading = false;
                    this.showError();
                });
    }

    showSuccess() {
        this.toastr.success(
            "Changed password successfuly!",
            "User updated: " + this.currentUser.userName,
        );
    }

    showError() {
        this.toastr.error(
            "Change password failed"
        );
    }

    onPasswordInput() {
        if (this.f.newPassword.value != this.f.newPassword2.value) {
            this.passwordForm.controls.newPassword2.setErrors([{'invalid': true}]);
        }
    }
}