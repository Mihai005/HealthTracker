package com.healthtracker.tracker_api.model;

import lombok.Getter;

@Getter
public class MealModel {
    private final String username;
    private final String type;
    private final String calories;

    public enum MealType {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }

    public MealModel(String username, String type, String calories) {
        this.username = username;
        this.type = type;
        this.calories = calories;
    }
}
