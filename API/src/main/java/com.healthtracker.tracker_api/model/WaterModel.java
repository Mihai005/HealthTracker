package com.healthtracker.tracker_api.model;

import lombok.Getter;

@Getter
public class WaterModel {
    private final String username;
    private final String quantity;

    public WaterModel(String username, String quantity) {
        this.username = username;
        this.quantity = quantity;
    }
}
