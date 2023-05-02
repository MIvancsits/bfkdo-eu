﻿using Common.Model;
using Database;
using Database.Tables;
using Microsoft.AspNetCore.Mvc;
using Common.Enums;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     Controller-Base für die BFKDO Basis.
    /// </summary>
    public class BfkdoControllerBase : ControllerBase
    {
        /// <summary>
        ///     Rufe das Testergebnis einer Testperson für Wissensteststufen ab.
        /// </summary>
        /// <param name="context">Datenbankkontext.</param>
        /// <param name="test">Angemeldeter Test.</param>
        /// <param name="person">Testperson.</param>
        /// <returns>Ergebnisse der Testung.</returns>
        protected List<ModelKnowledgeLevelResult> GetResultsForTestPerson(BfkdoDbContext context, TableKnowledgeTest test, TableTestperson person)
        {
            var list = new List<ModelKnowledgeLevelResult>();
            var registrations = test.Registrations.Where(t => t.TestpersonId == person.Id);
            foreach(var registration in registrations)
            {
                var passed = 0;
                var max = 0;
                foreach(var evaluation in registration.Evaluations)
                {
                    max++;
                    if(evaluation.Evaluation == EnumEvaluation.Passed && evaluation.EvaluationState == EnumEvaluationState.Closed)
                    {
                        passed++;
                    }
                }
                list.Add(new ModelKnowledgeLevelResult
                {
                    LevelName = registration.KnowledgeLevel.Description,
                    LevelResult = $"{passed} / {max}"
                });
            }

            return list;
        }
    }
}
