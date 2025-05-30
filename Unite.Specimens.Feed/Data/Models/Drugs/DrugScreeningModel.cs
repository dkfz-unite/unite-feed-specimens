﻿namespace Unite.Specimens.Feed.Data.Models.Drugs;

public class DrugScreeningModel
{
    public string Drug;
    
    public double? Gof;
    public double? Dss;
    public double? DssS;
    public double? DoseMin;
    public double? DoseMax;
    public double? Dose25;
    public double? Dose50;
    public double? Dose75;
    public double[] Doses;
    public double[] Responses;
}
