package com.healthtracker.tracker_api.controller;

import com.healthtracker.tracker_api.model.MealModel;
import com.healthtracker.tracker_api.model.StepsModel;
import com.healthtracker.tracker_api.model.WaterModel;
import com.healthtracker.tracker_api.service.DailyLogService;
import com.healthtracker.tracker_api.table.DailyLog;
import com.healthtracker.tracker_api.table.Meal;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/daily")
public class DailyLogController {
    private final DailyLogService dailyLogService;

    public DailyLogController(DailyLogService dailyLogService) {
        this.dailyLogService = dailyLogService;
    }

    @PostMapping("/meals/add")
    public ResponseEntity<?> addMeal(@RequestBody MealModel request) {
        try {
            Meal meal = this.dailyLogService.addMeal(request);
            return ResponseEntity.ok(meal);
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PostMapping("/water/add")
    public ResponseEntity<?> addWater(@RequestBody WaterModel request) {
        try {
            this.dailyLogService.addWater(request);
            return ResponseEntity.ok("");
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/get/{username}")
    public ResponseEntity<?> getDailyLog(@PathVariable String username) {
        try {
            DailyLog dailyLog = this.dailyLogService.getDailyLog(username);
            return ResponseEntity.ok(dailyLog);
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/meals/get/{username}")
    public ResponseEntity<?> getDailyLogMeals(@PathVariable String username) {
        try {
            List<Meal> meals = this.dailyLogService.getDailyLogMeals(username);
            return ResponseEntity.ok(meals);
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PostMapping("/steps/add")
    public ResponseEntity<?> addSteps(@RequestBody StepsModel request) {
        try {
            this.dailyLogService.addSteps(request);
            return ResponseEntity.ok("");
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }
}
