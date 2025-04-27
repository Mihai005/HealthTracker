package com.healthtracker.tracker_api.table;

import com.healthtracker.tracker_api.model.MealModel.MealType;
import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

@Getter
@Entity
@Table(name = "Meal", schema = "dbo")
public class Meal {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "mealId")
    private int mealId;

    @ManyToOne
    @JoinColumn(name = "logId", nullable = false)
    @Setter
    private DailyLog log;

    @Column(nullable = false)
    @Enumerated(EnumType.STRING)
    @Setter
    private MealType mealType;

    @Column(name = "calories", nullable = false)
    @Setter
    private int calories;
}
