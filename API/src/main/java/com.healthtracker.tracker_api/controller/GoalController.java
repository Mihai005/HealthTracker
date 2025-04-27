package com.healthtracker.tracker_api.controller;

import com.healthtracker.tracker_api.model.GoalModel;
import com.healthtracker.tracker_api.service.GoalService;
import com.healthtracker.tracker_api.table.Goal;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/goals")
public class GoalController {
    private final GoalService service;

    public GoalController(GoalService serv) {
        this.service = serv;
    }

    @PostMapping("/add")
    public ResponseEntity<?> addGoals(@RequestBody GoalModel request) {
        try {
            Goal goal = this.service.addGoals(request.getUsername(), request.getCalorieGoal(), request.getWaterGoal(),
                    request.getStepsGoal());
            return ResponseEntity.ok(goal);
        }
        catch(Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @GetMapping("/get/{username}")
    public ResponseEntity<?> getGoal(@PathVariable String username) {
        try {
            Goal goal = this.service.getGoal(username);
            return ResponseEntity.ok(goal);
        }
        catch (Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }

    @PostMapping("/update")
    public ResponseEntity<?> updateGoals(@RequestBody GoalModel request) {
        try {
            Goal goal = this.service.updateGoals(request.getUsername(), request.getCalorieGoal(), request.getWaterGoal(),
                    request.getStepsGoal());
            return ResponseEntity.ok(goal);
        }
        catch(Exception ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ex.getMessage());
        }
    }
}
