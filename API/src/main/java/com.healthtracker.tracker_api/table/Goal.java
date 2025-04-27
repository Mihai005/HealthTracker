package com.healthtracker.tracker_api.table;

import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Getter
@Entity
@Table(name = "Goal", schema = "dbo")
public class Goal {
    @Id
    @Column(name = "userId")
    private int userId;

    @Getter
    @Setter
    @Column(nullable = false, name = "calorieGoal")
    private int calorieGoal;
    @Getter
    @Setter
    @Column(nullable = false, name = "waterGoal")
    private int waterGoal;
    @Getter
    @Setter
    @Column(nullable = false, name = "stepsGoal")
    private int stepsGoal;

    @Setter
    @OneToOne
    @MapsId
    @JoinColumn(name = "userId")
    @JsonIgnore
    private User user;

    public Goal(int calorie, int water, int steps)
    {
        this.calorieGoal = calorie;
        this.waterGoal = water;
        this.stepsGoal = steps;
    }

    public Goal() {

    }
}
