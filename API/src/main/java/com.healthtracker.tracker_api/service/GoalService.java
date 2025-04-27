package com.healthtracker.tracker_api.service;

import com.healthtracker.tracker_api.repository.GoalRepository;
import com.healthtracker.tracker_api.repository.UserRepository;
import com.healthtracker.tracker_api.table.Goal;
import com.healthtracker.tracker_api.table.User;
import org.springframework.stereotype.Service;

import java.util.Arrays;
import java.util.List;


@Service
public class GoalService {

    private final UserRepository userRepository;

    private final GoalRepository goalRepository;

    public GoalService(UserRepository userRepository, GoalRepository goalRepository) {
        this.userRepository = userRepository;
        this.goalRepository = goalRepository;
    }

    public Goal addGoals(String username, String calorie, String water, String steps) throws Exception {
        List<Integer> result = this.InputValidation(username, calorie, water, steps);
        int calorieGoal = result.get(0);
        int waterGoal = result.get(1);
        int stepsGoal = result.get(2);

        User user = userRepository.findByUserName(username)
                .orElseThrow(() -> new RuntimeException("User not found"));
        Goal goal = new Goal(calorieGoal, waterGoal, stepsGoal);
        goal.setUser(user);
        return this.goalRepository.save(goal);
    }

    public Goal getGoal(String username) {
        User user = this.userRepository.findByUserName(username)
                .orElseThrow(() -> new RuntimeException("User not found"));
        return this.goalRepository.findByUserId(user.getUserId()).
                orElseThrow(() -> new RuntimeException("Goal not found"));
    }

    public Goal updateGoals(String username, String calorie, String water, String steps) throws Exception {

        List<Integer> result = this.InputValidation(username, calorie, water, steps);
        int calorieGoal = result.get(0);
        int waterGoal = result.get(1);
        int stepsGoal = result.get(2);

        User user = userRepository.findByUserName(username)
                .orElseThrow(() -> new RuntimeException("User not found"));

        Goal goal = goalRepository.findByUserId(user.getUserId())
                .orElseThrow(() -> new RuntimeException("Goal not found"));
        goal.setCalorieGoal(calorieGoal);
        goal.setWaterGoal(waterGoal);
        goal.setStepsGoal(stepsGoal);
        return this.goalRepository.save(goal);
    }

    private List<Integer> InputValidation(String username, String calorie, String water, String steps) throws Exception {
        int calorieGoal, waterGoal, stepsGoal;
        try {
            calorieGoal = Integer.parseInt(calorie);
        }
        catch(Exception ex) {
            throw new Exception("Calorie goal must be an integer");
        }
        try {
            waterGoal = Integer.parseInt(water);
        }
        catch(Exception ex) {
            throw new Exception("Water goal must be an integer");
        }
        try {
            stepsGoal = Integer.parseInt(steps);
        }
        catch(Exception ex) {
            throw new Exception("Steps goal must be an integer");
        }
        return Arrays.asList(calorieGoal, waterGoal, stepsGoal);
    }
}
