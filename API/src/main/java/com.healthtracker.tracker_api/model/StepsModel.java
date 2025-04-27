package com.healthtracker.tracker_api.model;

import lombok.Getter;

@Getter
public class StepsModel {
    private final String username;
    private final String steps;

    public StepsModel(String username, String steps) {
        this.username = username;
        this.steps = steps;
    }
}
