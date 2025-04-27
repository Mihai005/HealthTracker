package com.healthtracker.tracker_api.model;

import lombok.Getter;

@Getter
public class LogInModel {
    private final String username;
    private final String password;

    public LogInModel(String u, String p) {
        this.username = u;
        this.password = p;
    }
}
