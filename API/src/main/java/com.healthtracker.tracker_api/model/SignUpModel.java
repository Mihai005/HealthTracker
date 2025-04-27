package com.healthtracker.tracker_api.model;

import lombok.Getter;

@Getter
public class SignUpModel {
    private final String username;
    private final String password;
    private final String repeatPassword;

    public SignUpModel(String u, String p, String r) {
        this.username = u;
        this.password = p;
        this.repeatPassword = r;
    }
}
