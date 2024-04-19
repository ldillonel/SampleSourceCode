import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from 'src/app/services/authentication.service';
import { ToastrService } from 'ngx-toastr';

@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    hide = true;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        private toastr: ToastrService,
    ) {
    }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }

        this.loading = true;

        // Establish user credentials
        let userCred = {
            username: this.f.username.value,
            password: this.f.password.value
        };

        this.authenticationService.register(userCred)
            .pipe(first())
            .subscribe(
                data => {
                    // Register controller came back - data possibly null if user already exists
                    if (data) {
                        this.showSuccess();
                        this.error = '';
                        this.loading = false;
                    } else {
                        this.error = 'User already exists';
                        this.loading = false;
                    }
                },
                error => {
                    this.showError();
                    this.error = error;
                    this.loading = false;
                });
    }

    showSuccess() {
        this.toastr.success(
            "Registered user successfully!",
            "New user: " + this.f.username.value,
        );
    }

    showError() {
        this.toastr.error(
            "Register user failed"
        );
    }
}